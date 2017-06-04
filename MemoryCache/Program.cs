using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MemoryCacheDefault
{
    class Program
    {
        static void Main(string[] args)
        {
            //CategoryProcess();
            ItemProcess();

            Console.ReadKey();
        }

        #region Category Process

        static void CategoryProcess()
        {
            // create default categories
            var categories = GetCategoryList();


            // set default categories
            SetCategories(categories);


            // write categories
            WriteCategories();


            //Console.Clear();

            // add new category
            AddCategoryToCache("Peugeot", new List<string> { "206", "207" });

            //Console.Clear();

            // update category
            UpdateCategoryToCache("Peugeot", "Peugeot Updated");

            //Console.Clear();

            // remove category
            RemoveCategoryToCache("Peugeot Updated");

            Console.Clear();

            // get single category
            var category = GetCategoryFromCache("Opel");

            if (category != null)
            {
                Console.WriteLine("Name:" + category.Name);
                if (category.Items != null && category.Items.Any())
                {
                    foreach (var item in category.Items)
                    {
                        Console.WriteLine(" Item: " + item.Name);
                    }
                }
            }
        }

        static void SetCategories(List<Category> categories)
        {
            var cache = MemoryCache.Default;
            var currentCategories = GetListFromCache<Category>("categories");
            if (currentCategories != null)
            {
                cache.Remove("categories");
            }
            cache.Add("categories", categories, null);
        }

        static List<Category> GetCategoryList()
        {
            var categoryList = new List<Category>();
            categoryList.Add(new Category
            {
                Name = "Opel",
                Items = new List<Item>
                {
                    new Item { Name = "Astra" },
                    new Item { Name = "Insignia" },
                    new Item { Name = "Corsa" }
                }
            });
            categoryList.Add(new
                Category
            {
                Name = "Bmw",
                Items = new List<Item>
                {
                    new Item { Name = "5.20" },
                    new Item { Name = "3.20" },
                    new Item { Name = "3.18" }
                }
            });
            categoryList.Add(new
                Category
            {
                Name = "Audi",
                Items = new List<Item>
                {
                    new Item { Name = "A3" },
                    new Item { Name = "A4" }
                }
            });
            categoryList.Add(new Category
            {
                Name = "Mercedes",
                Items = new List<Item>
                {
                    new Item { Name = "C 180" },
                    new Item { Name = "A 180" },
                    new Item { Name = "S 500" } }
            });
            return categoryList;
        }

        static void AddCategoryToCache(string name, List<string> items)
        {
            var categories = GetListFromCache<Category>("categories");
            if (!categories.Any(s => s.Name.Equals(name)))
            {
                var category = new Category();
                category.Name = name;
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        category.Items.Add(new Item { Name = item });
                    }
                }
                categories.Add(category);

                SetCategories(categories);

                WriteCategories();
            }
            else
            {
                Console.WriteLine("This record already exists");
            }
        }

        static void UpdateCategoryToCache(string currentName, string updatedName)
        {
            var categories = GetListFromCache<Category>("categories");
            var category = categories.FirstOrDefault(s => s.Name.Equals(currentName));
            if (category != null)
            {
                var updatedCategory = category;
                updatedCategory.Name = updatedName;

                categories.Remove(category);

                categories.Add(updatedCategory);

                SetCategories(categories);

                WriteCategories();
            }
            else
            {
                Console.WriteLine("This record not found");
            }
        }


        static void RemoveCategoryToCache(string name)
        {
            var categories = GetListFromCache<Category>("categories");
            var category = categories.FirstOrDefault(s => s.Name.Equals(name));
            if (category != null)
            {
                categories.Remove(category);

                SetCategories(categories);

                WriteCategories();
            }
            else
            {
                Console.WriteLine("This record not found");
            }
        }

        static Category GetCategoryFromCache(string name)
        {
            var categories = GetListFromCache<Category>("categories");
            var category = categories.FirstOrDefault(s => s.Name.Equals(name));
            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        static void WriteCategories()
        {
            Console.WriteLine("Categories from cache");
            Console.WriteLine("");
            var categoriesFromCache = GetListFromCache<Category>("categories");
            if (categoriesFromCache != null)
            {
                foreach (var category in categoriesFromCache)
                {
                    Console.WriteLine("Name: " + category.Name);
                    var items = category.Items.ToList();
                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            Console.WriteLine(" Item: " + item.Name);
                        }
                        Console.WriteLine("");
                    }

                }
            }
        }

        #endregion

        #region Item Process

        static void ItemProcess()
        {
            // create default items
            var items = GetItemList();

            // set default items
            SetItems(items);

            // write items
            WriteItems();

            Console.Clear();

            AddItemToCache("Linea", "Fiat");

            Console.Clear();

            UpdateItemToCache("Linea", "Linea Updated");

            Console.Clear();

            RemoveItemToCache("Linea Updated");

            Console.Clear();

            var item = GetItemFromCache("A 180");

            if (item != null)
            {
                Console.WriteLine("Item: " + item.Name + ", Category: " + item.Category.Name);
            }
            else
            {
                Console.WriteLine("This record not found");
            }
        }

        static void SetItems(List<Item> items)
        {
            var cache = MemoryCache.Default;
            var currentItems = GetListFromCache<Item>("items");
            if (currentItems != null)
            {
                cache.Remove("items");
            }
            cache.Add("items", items, null);
        }

        static List<Item> GetItemList()
        {
            var itemList = new List<Item>();
            itemList.Add(new Item { Name = "Astra", Category = new Category { Name = "Opel" } });
            itemList.Add(new Item { Name = "Insignia", Category = new Category { Name = "Opel" } });
            itemList.Add(new Item { Name = "Corsa", Category = new Category { Name = "Opel" } });
            itemList.Add(new Item { Name = "5.20", Category = new Category { Name = "Bmw" } });
            itemList.Add(new Item { Name = "3.20", Category = new Category { Name = "Bmw" } });
            itemList.Add(new Item { Name = "3.18", Category = new Category { Name = "Bmw" } });
            itemList.Add(new Item { Name = "A3", Category = new Category { Name = "Audi" } });
            itemList.Add(new Item { Name = "A4", Category = new Category { Name = "Audi" } });
            itemList.Add(new Item { Name = "C 180", Category = new Category { Name = "Mercedes" } });
            itemList.Add(new Item { Name = "A 180", Category = new Category { Name = "Mercedes" } });
            itemList.Add(new Item { Name = "S 500", Category = new Category { Name = "Mercedes" } });
            return itemList;
        }

        static void AddItemToCache(string name, string category)
        {
            var items = GetListFromCache<Item>("items");
            if (items != null && !items.Any(s => s.Name.Equals(name)))
            {
                items.Add(new Item { Name = name, Category = new Category { Name = category } });
                SetItems(items);

                WriteItems();
            }
            else
            {
                Console.WriteLine("This record already exists");
            }
        }

        static void UpdateItemToCache(string currentName, string updatedName)
        {
            var items = GetListFromCache<Item>("items");
            var item = items.FirstOrDefault(s => s.Name.Equals(currentName));
            if (item != null)
            {
                var updatedCategory = item;
                updatedCategory.Name = updatedName;

                items.Remove(item);

                items.Add(updatedCategory);

                SetItems(items);

                WriteItems();
            }
            else
            {
                Console.WriteLine("This record not found");
            }
        }

        static void RemoveItemToCache(string name)
        {
            var items = GetListFromCache<Item>("items");
            var item = items.FirstOrDefault(s => s.Name.Equals(name));
            if (item != null)
            {
                items.Remove(item);

                SetItems(items);

                WriteItems();
            }
            else
            {
                Console.WriteLine("This record not found");
            }
        }

        static Item GetItemFromCache(string name)
        {
            var items = GetListFromCache<Item>("items");
            var item = items.FirstOrDefault(s => s.Name.Equals(name));
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        static void WriteItems()
        {
            Console.WriteLine("");

            Console.WriteLine("Items from cache");
            Console.WriteLine("");

            var itemsFromCache = GetListFromCache<Item>("items");
            if (itemsFromCache != null)
            {
                foreach (var item in itemsFromCache)
                {
                    Console.WriteLine("Name: " + item.Name + ", Category: " + item.Category.Name);
                }
            }
        }

        #endregion

        static List<T> GetListFromCache<T>(string name)
        {
            var cache = MemoryCache.Default;
            var list = (List<T>)cache.Get(name);
            return list;
        }
    }
}