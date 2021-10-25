using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Om.Chainlogger;

namespace Om.Tests
{
    /// <summary>
    /// Test of the Chainlogger
    /// You can test it from https://jupiter.omlira.com
    /// </summary>
    class TestCase
    {
        Logger logger;
        /// <summary>
        /// Main Method
        /// </summary>
        public void Main()
        {
            _ = initAsync();
        }

        /// <summary>
        /// Initialize of the program.
        /// </summary>
        /// <returns></returns>
        public async Task initAsync()
        {
            TestCase test = new TestCase();
            test.Setup();
            await test.TestRegisterVendorAsync();
            await test.TestRegisterProjectAsync();
            await test.TestRegisterLogAsync();
        }

        /// <summary>
        /// Setup the settings of chainlogger.
        /// </summary>
        public void Setup()
        {
            var abi = @"[{""type"":""constructor"",""stateMutability"":""nonpayable"",""inputs"":[]},{""type"":""event"",""name"":""LogRegistered"",""inputs"":[{""type"":""address"",""name"":""_vendorAddress"",""internalType"":""address"",""indexed"":true},{""type"":""uint256"",""name"":""_projectId"",""internalType"":""uint256"",""indexed"":false},{""type"":""uint256"",""name"":""_projectLogCounter"",""internalType"":""uint256"",""indexed"":false},{""type"":""bytes32"",""name"":""_data"",""internalType"":""bytes32"",""indexed"":true}],""anonymous"":false},{""type"":""event"",""name"":""VendorRegistered"",""inputs"":[{""type"":""uint256"",""name"":""_id"",""internalType"":""uint256"",""indexed"":true},{""type"":""address"",""name"":""_vendorAddress"",""internalType"":""address"",""indexed"":true}],""anonymous"":false},{""type"":""function"",""stateMutability"":""nonpayable"",""outputs"":[{""type"":""bool"",""name"":"""",""internalType"":""bool""}],""name"":""_changeOwner"",""inputs"":[{""type"":""address"",""name"":""toOwner"",""internalType"":""address""}]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""bytes32"",""name"":"""",""internalType"":""bytes32""}],""name"":""getLog"",""inputs"":[{""type"":""address"",""name"":""vendorAddress"",""internalType"":""address""},{""type"":""uint256"",""name"":""projectId"",""internalType"":""uint256""},{""type"":""uint256"",""name"":""logId"",""internalType"":""uint256""}]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}],""name"":""numVendors"",""inputs"":[]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""address"",""name"":"""",""internalType"":""address""}],""name"":""owner"",""inputs"":[]},{""type"":""function"",""stateMutability"":""nonpayable"",""outputs"":[{""type"":""address"",""name"":"""",""internalType"":""address""},{""type"":""uint256"",""name"":"""",""internalType"":""uint256""},{""type"":""uint256"",""name"":"""",""internalType"":""uint256""},{""type"":""bytes32"",""name"":"""",""internalType"":""bytes32""}],""name"":""registerLog"",""inputs"":[{""type"":""uint256"",""name"":""projectId"",""internalType"":""uint256""},{""type"":""bytes32"",""name"":""data"",""internalType"":""bytes32""}]},{""type"":""function"",""stateMutability"":""nonpayable"",""outputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}],""name"":""registerProject"",""inputs"":[]},{""type"":""function"",""stateMutability"":""nonpayable"",""outputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}],""name"":""registerVendor"",""inputs"":[]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""address"",""name"":""vendorAddress"",""internalType"":""address""},{""type"":""uint256"",""name"":""projectCounter"",""internalType"":""uint256""}],""name"":""vendorLogs"",""inputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""address"",""name"":"""",""internalType"":""address""}],""name"":""vendors"",""inputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}]},{""type"":""function"",""stateMutability"":""view"",""outputs"":[{""type"":""uint256"",""name"":"""",""internalType"":""uint256""}],""name"":""vendorsReverse"",""inputs"":[{""type"":""address"",""name"":"""",""internalType"":""address""}]}]";
            var provider = "https://provider.omlira.com";
            var ethSigner = "http://localhost:8561";
            var contractAddress = "0xABCDcB8369dCddA5c91f9DBa664F891621B5bc6D";

            var logger = new Logger();
            logger.SetProvider(provider);
            logger.SetAbi(abi);
            logger.SetEthSigner(ethSigner);
            logger.SetWeb3();
            logger.SetSalt("MySecretSalt");
            logger.SetContract(contractAddress);
            logger.SetSendContract(contractAddress);
            logger.SetAccount("0x6E6511ed824202a9d7D1D4D2C35544fDd5A551E2");
            this.logger = logger;
        }

        /// <summary>
        /// RegisterLog function
        /// </summary>
        /// <returns></returns>
        public async Task TestRegisterLogAsync()
        {
            var rawInput = "MyTestInput";
            var txHash = await this.logger.RegisterLogAsync(0, rawInput);
            Console.WriteLine(txHash.ToString());
        }

        /// <summary>
        /// RegisterVendor function
        /// </summary>
        /// <returns></returns>
        public async Task TestRegisterVendorAsync()
        {
            var txHash = await this.logger.RegisterVendorAsync();
            Console.WriteLine(txHash.ToString());
        }

        /// <summary>
        /// RegisterProject function
        /// </summary>
        /// <returns></returns>
        public async Task TestRegisterProjectAsync()
        {
            var txHash = await this.logger.RegisterProjectAsync();
            Console.WriteLine(txHash.ToString());
        }
    }
}
