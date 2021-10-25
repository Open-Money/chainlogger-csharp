using Nethereum.Web3;
using System;

namespace Om.Chainlogger
{
    /// <summary>
    /// A class to initialize contracts, gets the provider and abi information about the contract. Initializes web3 instances.
    /// ....
    /// Methods
    /// ....
    /// set_provider(string)
    /// set_abi(string)
    /// set_eth_signer(string)
    /// set_contract(string)
    /// set_send_contract(string)
    /// set_web3(string)
    /// get_contract()
    /// get_send_contract()
    /// get_web3()
    /// </summary>
    class Contract
    {
        public string contractAddress = "0x27977679d45bdB739E1cdd9A5c510B471CA0aB75";
        public string provider;
        public string abi;
        public string ethSigner;
        public Nethereum.Contracts.Contract contract;
        public Nethereum.Contracts.Contract sendContract;
        Web3 web3;


        /// <summary>
        /// Checks all the variables exists
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="abi"></param>
        public static void CheckConnectionUrls(string provider, string abi)
        {
            if (provider == null)
            {
                Exception exception = new Exception("You must set the provider first");
            }

            if (abi == null)
            {
                Exception exception = new Exception("You must set the abi of the contract first");
            }
        }


        /// <summary>
        /// Sets the provider
        /// </summary>
        /// <param name="provider"></param>
        public void SetProvider(string provider)
        {
            this.provider = provider;
        }


        /// <summary>
        /// Sets the abi
        /// </summary>
        /// <param name="abi"></param>
        public void SetAbi(string abi)
        {
            this.abi = abi;
        }

        /// <summary>
        /// Sets the ethSigner
        /// </summary>
        /// <param name="ethSigner"></param>
        public void SetEthSigner(string ethSigner)
        {
            this.ethSigner = ethSigner;
        }

        /// <summary>
        /// Sets the deployed contract
        /// </summary>
        /// <param name="at_address"></param>
        public void SetContract(string atAddress)
        {
            CheckConnectionUrls(this.provider, this.abi);
            var webInstance = new Web3(this.provider);

            this.contract = webInstance.Eth.GetContract(this.abi, atAddress);
        }

        /// <summary>
        /// Sets the transaction sending contract using EthSigner provider
        /// </summary>
        public void SetSendContract(string sendContract)
        {
            CheckConnectionUrls(this.provider, this.abi);
            var ethSignerInstance = new Web3(this.ethSigner);

            this.sendContract = ethSignerInstance.Eth.GetContract(abi, sendContract);
        }

        /// <summary>
        /// eth_signer (string): eth signer url
        /// </summary>
        /// <returns>value (Web3)</returns>
        public Web3 SetSendInstance()
        {
            return new Web3(this.ethSigner);
        }

        /// <summary>
        /// Sets the Web3 object
        /// </summary>
        public void SetWeb3()
        {
            if (this.provider == null)
            {
                Exception exception = new Exception("You must set the provider first");
            }

            this.web3 = new Web3(this.provider);
        }

        /// <summary>
        /// Gets the contract
        /// </summary>
        /// <returns>value (Nethereum.Contracts.Contract)</returns>
        public Nethereum.Contracts.Contract GetContract()
        {
            return this.contract;
        }

        /// <summary>
        /// Gets the contract
        /// </summary>
        /// <returns>value (Nethereum.Contracts.Contract)</returns>
        public Nethereum.Contracts.Contract GetSendContract()
        {
            return this.sendContract;
        }

        /// <summary>
        /// Gets the web3 object
        /// </summary>
        /// <returns>value (Web3)</returns>
        public Web3 GetWeb3()
        {
            return this.web3;
        }
    }
}
