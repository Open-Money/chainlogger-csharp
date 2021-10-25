using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Om.Chainlogger
{
    /// <summary>
    /// A class to contain logging logics.
    ///    ...
    ///    Methods
    ///    -------
    ///    create()
    ///    set_gas(string)
    ///    set_gas_price(string)
    ///    set_account(string)
    ///    set_salt(string)
    ///    register_vendor()
    ///    register_project()
    ///    register_log(int, string)
    ///    get_transaction_receipt(string)
    ///    get_batch_transaction_receipt(list)
    ///    parse_data(string)
    ///    parse_batch_data(list)
    ///    verify_data(string, string, string)
    /// </summary>
    class Logger : Contract
    {
        static int index;
        /// <summary>
        /// RegisterVendor class
        /// </summary>
        [Function("registerVendor")]
        public class RegisterVendor : FunctionMessage
        {

        }
        /// <summary>
        /// RegisterProject class
        /// </summary>
        [Function("registerProject")]
        public class RegisterProject : FunctionMessage
        {

        }

        /// <summary>
        /// RegisterLog class
        /// </summary>
        [Function("registerLog")]
        public class RegisterLog : FunctionMessage
        {
            [Parameter("uint256", "projectId", 1)]
            public BigInteger ProjectId { get; set; }

            [Parameter("bytes32", "data", 2)]
            public byte[] Data { get; set; }
        }

        /// <summary>
        /// LogRegistered class
        /// </summary>
        [Event("LogRegistered")]
        public class LogRegistered : IEventDTO
        {
            [Parameter("address", "_vendorAddress", 1, true)]
            public string _vendorAddress { get; set; }

            [Parameter("uint256", "_projectId", 2, false)]
            public BigInteger _projectId { get; set; }

            [Parameter("uint256", "_projectLogCounter", 3, false)]
            public BigInteger _projectLogCounter { get; set; }

            [Parameter("bytes32", "_data", 4, true)]
            public byte[] _data { get; set; }
        }
        protected string account;

        protected string salt = "Change_Me";

        protected BigInteger gas = 250000;

        protected BigInteger gasPrice = 100000000000;

        /// <summary>
        /// Static function that returns the Logger instance.
        /// </summary>
        /// <returns></returns>
        public static Logger create()
        {
            return new Logger();
        }

        /// <summary>
        /// Sets the gas.
        /// </summary>
        /// <param name="gas"></param>
        public void SetGas(BigInteger gas)
        {
            this.gas = gas;
        }

        /// <summary>
        /// Sets the gas price.
        /// </summary>
        /// <param name="gasPrice"></param>
        public void SetGasPrice(BigInteger gasPrice)
        {
            this.gasPrice = gasPrice;
        }

        /// <summary>
        /// Sets the user's account from EthSigner.
        /// </summary>
        /// <param name="account"></param>
        public void SetAccount(string account)
        {
            this.account = account;
        }

        /// <summary>
        /// Sets the salt.
        /// </summary>
        /// <param name="salt"></param>
        public void SetSalt(string salt)
        {
            this.salt = salt;
        }

        /// <summary>
        /// Calls the registerVendor method on the omChain
        ///Returns the transaction hash or throws error
        ///Takes gasPrice and gas as hexadecimal
        /// </summary>
        /// <returns>value (string): transaction hash</returns>
        public async Task<string> RegisterVendorAsync()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("from", this.account);
            parameters.Add("gasPrice", this.gasPrice);
            parameters.Add("gas", this.gas);

            var registerVendor = sendContract.GetFunction("registerVendor");
            var transfer = new RegisterVendor()
            {
                FromAddress = this.account,
                Gas = this.gas,
                GasPrice = this.gasPrice
            };
            var transferHandler = SetSendInstance().Eth.GetContractTransactionHandler<RegisterVendor>();

            var secondTransactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(this.contractAddress, transfer);

            return secondTransactionReceipt.TransactionHash;


        }

        /// <summary>
        /// Registers a project
        /// </summary>
        /// <returns>value (string): transaction hash</returns>
        public async Task<string> RegisterProjectAsync()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("from", this.account);
            parameters.Add("gasPrice", this.gasPrice);
            parameters.Add("gas", this.gas);


            var registerProject = sendContract.GetFunction("registerProject");
            var transfer = new RegisterProject()
            {
                FromAddress = this.account,
                Gas = this.gas,
                GasPrice = this.gasPrice
            };
            var transferHandler = SetSendInstance().Eth.GetContractTransactionHandler<RegisterProject>();

            var secondTransactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(this.contractAddress, transfer);

            return secondTransactionReceipt.TransactionHash;


        }

        /// <summary>
        /// Registers a log
        /// </summary>
        /// <param name="_projectId"></param>
        /// <param name="my_data"></param>
        /// <returns>value (string): transaction hash</returns>
        public async Task<string> RegisterLogAsync(int _projectId, object my_data)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("from", this.account);
            parameters.Add("gasPrice", this.gasPrice);
            parameters.Add("gas", this.gas);

            var hasher = new Hasher(this.salt);
            var toChainData = Parser.JsonEncode(my_data.ToString().Replace("\"", string.Empty));
            toChainData = hasher.HashWithSalt(toChainData);
            //toChainData = Hasher.add0x(toChainData);

            var datas = StringToByteArray(toChainData);


            var registerLog = sendContract.GetFunction("registerLog");
            var transfer = new RegisterLog()
            {
                FromAddress = this.account,
                Gas = this.gas,
                GasPrice = this.gasPrice,
                ProjectId = _projectId,
                Data = datas
            };
            var transferHandler = SetSendInstance().Eth.GetContractTransactionHandler<RegisterLog>();

            var secondTransactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(this.contractAddress, transfer);

            return secondTransactionReceipt.TransactionHash;
        }


        /// <summary>
        /// Gets transaction receipt with events
        /// </summary>
        /// <param name="txId"></param>
        /// <returns>value (dict): data dictionary</returns>
        public async Task<object> GetTransactionReceiptAsync(string txId)
        {
            var receipt = await GetContract().Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txId);
            var result = receipt.DecodeAllEvents<LogRegistered>();
            Console.WriteLine(result[0].Event._data.ToHex());
            Dictionary<object, object> dataDict = new Dictionary<object, object>();
            dataDict.Add("_data", result[0].Event._data);
            dataDict.Add("_projectId", result[0].Event._projectId);
            dataDict.Add("_projectLogCounter", result[0].Event._projectLogCounter);
            dataDict.Add("_vendorAddress", result[0].Event._vendorAddress);

            Dictionary<object, object> eventLogDataLs = new Dictionary<object, object>();
            eventLogDataLs.Add("transactionHash", result[0].Log.TransactionHash);
            eventLogDataLs.Add("blockHash", result[0].Log.BlockHash);
            eventLogDataLs.Add("blockNumber", result[0].Log.BlockNumber);
            eventLogDataLs.Add("data", dataDict);

            return eventLogDataLs;
        }

        /// <summary>
        /// Get Batch Transaction Receipts
        /// </summary>
        /// <param name="txIds"></param>
        /// <returns>value (array): data dictionary list</returns>
        public async Task<object[]> GetBatchTransactionReceiptAsync(string[] txIds)
        {
            object[] returnArray = new object[] { };
            for (int i = 0; i < txIds.Length; i++)
            {
                returnArray[i] = (string)await GetTransactionReceiptAsync(txIds[i]);
            }
            return returnArray;
        }

        /// <summary>
        /// Parses the transaction receipt's data column
        ///to return string instead of hexadecimal
        /// </summary>
        /// <param name="blockData"></param>
        /// <returns>value (list): parsed data</returns>
        public static object[] ParseData(object[] blockData)
        {
            index = 0;
            object[] returnData = { };
            foreach (string value in blockData)
            {
                if (typeof(BigInteger).IsInstanceOfType(value))
                {
                    returnData[index] = value.ToString();
                }
                else
                {
                    returnData[index] = value;
                }
                index++;
            }
            return returnData;
        }

        /// <summary>
        /// Parse Batch Block Data
        /// </summary>
        /// <param name="blockDatas"></param>
        /// <returns>value (list): parsed data list</returns>
        public static object[] ParseBatchData(object[] blockDatas)
        {
            object[] returnData = { };
            for (int i = 0; i < blockDatas.Length; i++)
            {
                returnData.Append(ParseData(blockDatas));
            }
            return returnData;
        }

        /// <summary>
        /// Verify integrity of blockchain record
        /// </summary>
        /// <param name="hashedData"></param>
        /// <param name="rawInput"></param>
        /// <param name="salt"></param>
        /// <returns>value (bool): is verify value</returns>
        public bool VerifyData(string hashedData, object rawInput, string salt)
        {
            Hasher hash;
            if (salt == null)
            {
                hash = Hasher.Create(salt);
            }
            else
            {
                hash = Hasher.Create(this.salt);
            }

            var toChainData = Parser.JsonEncode(rawInput);
            toChainData = hash.HashWithSalt(toChainData);
            //toChainData = Hasher.Add0x(toChainData);

            if (hashedData == toChainData)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// It change from hex to bytes.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>byte array</returns>
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
