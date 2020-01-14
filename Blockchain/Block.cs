using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    [DataContract]
    public class Block
    {
        //Идентификатор
        public int Id { get; private set; }
        //Данные
        [DataMember]
        public string Data { get; private set; }
        //Дата и время создания
        [DataMember]
        public DateTime Created { get; private set; }
        //Хэш блока
        [DataMember]
        public string Hash { get; private set; }
        //Хэш предыдущего блока
        [DataMember]
        public string PreviousHash { get; private set; }
        //Имя пользователя
        [DataMember]
        public string User { get; set; }

        //Конструктор для генезис-блока
        public Block()
        {
            Id = 1;
            Data = "Hello, world";
            Created = DateTime.Parse("12.01.2020 00:00:00.000");
            PreviousHash = "111111";
            User = "Admin";

            var data = GetData();
            Hash = GetHash(data);
        }

        /// <summary>
        /// Конструктор блока
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="block"></param>
        public Block(string data, string user, Block block)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException($"Пустой аргумент data", nameof(data));
            }
            if (block == null)
            {
                throw new ArgumentNullException($"Пустой аргумент block", nameof(block));
            }
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException($"Пустой аргумент user", nameof(user));
            }

            Data = data;
            User = user;
            PreviousHash = block.Hash;
            Created = DateTime.UtcNow;
            Id = block.Id + 1;
            var blockData = GetData();
            Hash = GetHash(blockData);
        }
        /// <summary>
        /// Получение значимых данных
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            string result = "";

            result += Data;
            result += Created.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += PreviousHash;
            result += User;

            return result;
        }
        /// <summary>
        /// Хэширование данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetHash(string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            var hashString = new SHA256Managed();
            string hex = "";
            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        public override string ToString()
        {
            return Data;
        }

        /// <summary>
        /// Выполнить сериализацию объекта в json строку
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));
            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        /// <summary>
        ///  Выполнить десериализацию объекта Block из json строки 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Block Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var result = (Block)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }
}
