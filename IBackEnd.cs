using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBackEnd" in both code and config file together.
    [ServiceContract]
    public interface IBackEnd
    {
        // User Functionalities.
        [OperationContract]
        int Register(User user);

        [OperationContract]
        bool IsRegistered(string email, string password);

        [OperationContract]
        bool Login(string email, string password);

        [OperationContract]
        User GetUser(String email);

        [OperationContract]
        User GetUserToUpdate(String ID);

        [OperationContract]
        int FindUser(string email);

        [OperationContract]
        bool UpdateUser(User user, int id);

        // Product functionality
        [OperationContract]
        List<productTable> GetAllProducts();

        [OperationContract]
        List<productTable> GetAllKotaProducts();

        [OperationContract]
        List<productTable> GetALlBunnyProducts();

        [OperationContract]
        List<productTable> GetProducts();

        [OperationContract]
        bool AddNewProduct(Product product);

        [OperationContract]
        bool DoesProductExist(string p_code);

        [OperationContract]
        int GetProductID(String ID);

        [OperationContract]
        Product GetProduct(string ID);

        [OperationContract]
        bool RemoveProduct(string id);

        [OperationContract]
        List<productTable> UpdateProducts();

        [OperationContract]
        bool UpdateProductByID(Product product, int id);

        [OperationContract]
        List<productTable> GetOnSpecial();

        [OperationContract]
        productTable AboutProduct(string id);
    }
}
