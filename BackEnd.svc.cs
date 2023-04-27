using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BackEnd" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BackEnd.svc or BackEnd.svc.cs at the Solution Explorer and start debugging.
    public class BackEnd : IBackEnd
    {
        KotaDataContext db = new KotaDataContext();

        public User GetUser(string email)
        {
            var find_user = (from u in db.userTables
                             where u.Email.Equals(email)
                             select u).FirstOrDefault();

            User user = new User
            {
                firstname = find_user.Firstname,
                lastname = find_user.Lastname,
                email = find_user.Email,
                type = find_user.userType
            };

            return user;
        }

        public User GetUserToUpdate(String ID)
        {
            var check_user = (from u in db.userTables
                              where u.Id.Equals(ID)
                              select u).FirstOrDefault();

            User user = new User
            {
                firstname = check_user.Firstname,
                lastname = check_user.Lastname,
                contact = check_user.Contact,
                email = check_user.Email,
                password = check_user.Password,
                type = check_user.userType,
                active = check_user.userActive
            };

            return user;
        }

        public bool IsRegistered(string email, string password)
        {
            dynamic check_user = (from u in db.userTables
                                  where u.Email.Equals(email) && u.Password.Equals(Secrecy.HashPassword(password))
                                  select u).FirstOrDefault();

            if (check_user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Login(string email, string password)
        {
            dynamic user_login = (from u in db.userTables
                                  where u.Email.Equals(email) && u.Password.Equals(Secrecy.HashPassword(password))
                                  select u).FirstOrDefault();

            if (user_login != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Register(User user)
        {
            var new_user = new userTable
            {
                Firstname = user.firstname,
                Lastname = user.lastname,
                Contact = user.contact,
                Email = user.email,
                Password = Secrecy.HashPassword(user.password),
                userType = user.type,
                userActive = user.active
            };

            db.userTables.InsertOnSubmit(new_user);

            try
            {
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return -1;
            }
        }

        public int FindUser(string email)
        {
            dynamic find_user = (from u in db.userTables
                                 where u.Email.Equals(email)
                                 select u).FirstOrDefault();

            if (find_user != null)
            {
                return find_user.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool UpdateUser(User user, int id)
        {
            var update_user = (from u in db.userTables
                               where u.Id.Equals(id)
                               select u).FirstOrDefault();

            update_user.Firstname = user.firstname;
            update_user.Lastname = user.lastname;
            update_user.Contact = user.contact;
            update_user.Email = user.email;
            update_user.Password = Secrecy.HashPassword(user.password);
            update_user.userType = user.type;
            update_user.userActive = user.active;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public List<productTable> GetAllProducts()
        {
            var list = new List<productTable>();

            dynamic items = (from p in db.productTables
                             where p.Active == 1
                             orderby p.Price
                             select p);

            foreach (productTable i in items)
            {
                list.Add(i);
            }

            return list;
        }

        public List<productTable> GetAllKotaProducts()
        {
            var list = new List<productTable>();

            dynamic items = (from p in db.productTables
                             where p.Type.Equals(0)
                             orderby p.Price
                             select p);

            foreach (productTable i in items)
            {
                list.Add(i);
            }

            return list;
        }

        public List<productTable> GetALlBunnyProducts()
        {
            var list = new List<productTable>();

            dynamic items = (from p in db.productTables
                             where p.Type.Equals(1)
                             orderby p.Price
                             select p);

            foreach (productTable i in items)
            {
                list.Add(i);
            }

            return list;
        }

        public List<productTable> GetProducts()
        {
            var list = new List<productTable>();

            dynamic items = (from p in db.productTables
                             orderby p.Code
                             select p);

            foreach (productTable i in items)
            {
                list.Add(i);
            }

            return list;
        }

        public bool AddNewProduct(Product product)
        {
            var new_product = new productTable
            {
                Code = product.code,
                Name = product.name,
                Price = product.price,
                Special = product.special,
                Description = product.description,
                Update = product.update,
                Image = product.image,
                Type = product.type,
                Active = product.active
            };

            db.productTables.InsertOnSubmit(new_product);

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public bool RemoveProduct(string id)
        {
            dynamic delete_product = (from p in db.productTables
                                      where p.Code.Equals(id)
                                      select p).FirstOrDefault();

            db.productTables.DeleteOnSubmit(delete_product);

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public List<productTable> UpdateProducts()
        {
            var product_list = new List<productTable>();

            dynamic update_product = (from p in db.productTables
                                      where p.Update.Equals(1)
                                      select p);

            foreach (productTable i in update_product)
            {
                product_list.Add(i);
            }

            return product_list;
        }

        public bool UpdateProductByID(Product product, int id)
        {
            var update_product = (from p in db.productTables
                                  where p.Code.Equals(id)
                                  select p).FirstOrDefault();

            update_product.Name = product.name;
            update_product.Price = product.price;
            update_product.Special = product.special;
            update_product.Description = product.description;
            update_product.Update = product.update;
            update_product.Image = product.image;
            update_product.Type = product.type;
            update_product.Active = product.active;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public List<productTable> GetOnSpecial()
        {
            var item_list = new List<productTable>();

            dynamic item = (from p in db.productTables
                            where p.Special.Equals(1)
                            orderby p.Price
                            select p);

            foreach (productTable p in item)
            {
                item_list.Add(p);
            }

            return item_list;
        }

        public bool DoesProductExist(string p_code)
        {
            dynamic product_exist = (from p in db.productTables
                                     where p.Code.Equals(p_code)
                                     select p).FirstOrDefault();

            if (product_exist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetProductID(String ID)
        {
            var get_product_id = (from p in db.productTables
                                  where p.Code.Equals(ID)
                                  select p).FirstOrDefault();

            if (get_product_id != null)
            {
                return get_product_id.Code;
            }
            else
            {
                return 0;
            }
        }

        public Product GetProduct(string ID)
        {
            var get_product = (from p in db.productTables
                               where p.Code.Equals(ID)
                               select p).FirstOrDefault();

            Product product = new Product
            {
                name = get_product.Name,
                price = Convert.ToInt32(get_product.Price),
                special = get_product.Special,
                description = get_product.Description,
                update = get_product.Update,
                image = get_product.Image,
                type = get_product.Type,
                active = get_product.Active
            };

            return product;
        }

        public productTable AboutProduct(string id)
        {
            dynamic items = (from p in db.productTables
                             where p.Code.Equals(id)
                             select p).FirstOrDefault();

            if (items != null)
            {
                return items;
            }
            else
            {
                return null;
            }
        }
    }
}
