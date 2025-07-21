using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseCrudApp
{
    internal class Program
    {
        static string connectionString = "Data Source=DESKTOP-0S58J1T\\SQLEXPRESS;Initial Catalog=EgitimKampiDb;Integrated Security=True";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***** VERİTABANI CRUD PANELİ *****\n");
                Console.WriteLine("1 - Kategori Ekle");
                Console.WriteLine("2 - Ürün Ekle");
                Console.WriteLine("3 - Ürün Listele");
                Console.WriteLine("4 - Ürün Güncelle");
                Console.WriteLine("5 - Ürün Sil");
                Console.WriteLine("6 - Çıkış");
                Console.Write("\nLütfen bir seçenek girin (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCategory();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        ListProducts();
                        break;
                    case "4":
                        UpdateProduct();
                        break;
                    case "5":
                        DeleteProduct();
                        break;
                    case "6":
                        Console.WriteLine("Çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }

                Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }

        static void AddCategory()
        {
            Console.Write("Kategori Adı: ");
            string categoryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TblCategory (CategoryName) VALUES (@name)", connection);
                cmd.Parameters.AddWithValue("@name", categoryName);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Kategori başarıyla eklendi.");
            }
        }

        static void AddProduct()
        {
            Console.Write("Ürün Adı: ");
            string name = Console.ReadLine();
            Console.Write("Ürün Fiyatı: ");
            decimal price = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TblProduct (ProductName, ProductPrice, ProductStatus) VALUES (@name, @price, @status)", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@status", true); // Varsayılan aktif
                cmd.ExecuteNonQuery();
                Console.WriteLine("Ürün başarıyla eklendi.");
            }
        }

        static void ListProducts()
        {
            Console.Clear();
            Console.WriteLine("----- ÜRÜN LİSTELEME PANELİ -----");
            Console.WriteLine("1 - Tüm Ürünleri Listele");
            Console.WriteLine("2 - Ürün Adına Göre Filtrele");
            Console.WriteLine("3 - Fiyat Aralığına Göre Filtrele");
            Console.WriteLine("4 - Duruma Göre Filtrele (Aktif/Pasif)");
            Console.Write("Filtreleme seçeneğinizi seçin (1-4): ");
            string filterChoice = Console.ReadLine();

            string query = "SELECT * FROM TblProduct";
            SqlCommand cmd;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                switch (filterChoice)
                {
                    case "1":
                        // Tüm ürünleri listele
                        cmd = new SqlCommand(query, connection);
                        break;

                    case "2":
                        // Ürün adına göre filtrele
                        Console.Write("Filtrelemek istediğiniz ürün adı (kısmi yazılabilir): ");
                        string productName = Console.ReadLine();
                        query += " WHERE ProductName LIKE @name";
                        cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@name", "%" + productName + "%");
                        break;

                    case "3":
                        // Fiyat aralığına göre filtrele
                        Console.Write("Minimum fiyat: ");
                        decimal minPrice = decimal.Parse(Console.ReadLine());
                        Console.Write("Maksimum fiyat: ");
                        decimal maxPrice = decimal.Parse(Console.ReadLine());
                        query += " WHERE ProductPrice BETWEEN @min AND @max";
                        cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@min", minPrice);
                        cmd.Parameters.AddWithValue("@max", maxPrice);
                        break;

                    case "4":
                        // Duruma göre filtrele
                        Console.Write("Aktif ürünleri listelemek için 1, pasif ürünler için 0 yazın: ");
                        bool status = Console.ReadLine() == "1";
                        query += " WHERE ProductStatus = @status";
                        cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@status", status);
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim. Tüm ürünler listeleniyor.");
                        cmd = new SqlCommand(query, connection);
                        break;
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Console.WriteLine("\n--- Ürün Listesi ---\n");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"ID: {row["ProductId"]} | Ad: {row["ProductName"]} | Fiyat: {row["ProductPrice"]} | Durum: {(Convert.ToBoolean(row["ProductStatus"]) ? "Aktif" : "Pasif")}");
                }
            }
        }


        static void UpdateProduct()
        {
            Console.Write("Güncellenecek Ürün ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Yeni Ürün Adı: ");
            string name = Console.ReadLine();
            Console.Write("Yeni Ürün Fiyatı: ");
            decimal price = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE TblProduct SET ProductName = @name, ProductPrice = @price WHERE ProductId = @id", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@id", id);
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{affected} ürün güncellendi.");
            }
        }

        static void DeleteProduct()
        {
            Console.Write("Silinecek Ürün ID: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM TblProduct WHERE ProductId = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{affected} ürün silindi.");
            }
        }
    }
}
