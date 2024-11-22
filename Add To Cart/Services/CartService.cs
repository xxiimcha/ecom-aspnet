using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        
        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // checker if product is already in the cart
        public async Task<bool> CheckIfProductInCart(int productId, string userID)
        {
            var existingCartItems = await GetCartItemsAsync(userID);
            return existingCartItems.Any(item => item.product_id == productId);
        }

        public async Task AddToCart(int productId, string userID)
        {
            bool itemExists = await CheckIfProductInCart(productId, userID);

            if (itemExists)
            {
                Console.WriteLine($"Product with ID {productId} is already in the cart.");
                return;
            }

            var cartItem = new CartItemModel
            {
                product_id = productId,
                UserID = userID,
                quantity = 1 
            };
            
            Console.WriteLine($"Adding item to cart: {JsonSerializer.Serialize(cartItem)}");

            var jsonContent = new StringContent(JsonSerializer.Serialize(cartItem), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://prof-elec.vercel.app/cart/{userID}", jsonContent);

            Console.WriteLine($"Adding item to cart: {JsonSerializer.Serialize(cartItem)}");
            Console.WriteLine($"Received cart item with product_id: {cartItem.product_id}");


            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to add item to cart. Status Code: {response.StatusCode}, Error: {errorContent}");
                throw new HttpRequestException($"Failed to add item to cart. {response.ReasonPhrase}");
            }
            else
            {
                Console.WriteLine($"Item with ID {productId} successfully added to the cart.");
            }
        }


        public async Task UpdateCartItemQuantity(int productId, string userID, int quantity)
        {

            Console.WriteLine($"User ID: {userID}");
            var url = $"https://prof-elec.vercel.app/cart/{userID}";

            var jsonData = new
            {
                product_id = productId,
                quantity = quantity
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(jsonData), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PutAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Success: {responseBody}");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to update cart item. Status Code: {response.StatusCode}. Error Response: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating cart item: {ex.Message}");
            }
            
        }
        
        public async Task RemoveFromCart(int productId, string userID)
        {
            var response = await _httpClient.DeleteAsync($"https://prof-elec.vercel.app/cart/{userID}/{productId}");


            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to remove item from cart. Status code: {response.StatusCode}");
            }
        }


        // this will get the cart items for a user
        public async Task<List<CartItemModel>> GetCartItemsAsync(string userID)
        {
            var response = await _httpClient.GetAsync($"https://prof-elec.vercel.app/cart/{userID}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cartItems = JsonSerializer.Deserialize<List<CartItemModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return cartItems ?? new List<CartItemModel>();  
            }

            return new List<CartItemModel>(); 
        }

     
        
       
    }
}
