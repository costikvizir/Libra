using System;
using System.Runtime.Serialization;

namespace LibraWebApp.GenericModels{
    /// <summary>
    /// Info about currently logged user
    /// </summary>
    
    [Serializable]
    [DataContract]
    public class UserInfo : IUserInformation {
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// UserName - maticola
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        //[DataMember]
        //public string FullName { get; set; }
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }

    public interface IUserInformation
    {
        int Id { get; set; }
        /// <summary>
        /// UserName - maticola
        /// </summary>
        string UserName { get; set; }
        //string FullName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        bool IsEnabled { get; set; }
    }
}
