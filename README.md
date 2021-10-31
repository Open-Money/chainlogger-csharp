# chainlogger-csharp
Simple csharp package for using Chain Logger on omChain Jupiter

### ABI:
```json
[{"type":"constructor","stateMutability":"nonpayable","inputs":[]},{"type":"event","name":"LogRegistered","inputs":[{"type":"address","name":"_vendorAddress","internalType":"address","indexed":true},{"type":"uint256","name":"_projectId","internalType":"uint256","indexed":false},{"type":"uint256","name":"_projectLogCounter","internalType":"uint256","indexed":false},{"type":"bytes32","name":"_data","internalType":"bytes32","indexed":true}],"anonymous":false},{"type":"event","name":"VendorRegistered","inputs":[{"type":"uint256","name":"_id","internalType":"uint256","indexed":true},{"type":"address","name":"_vendorAddress","internalType":"address","indexed":true}],"anonymous":false},{"type":"function","stateMutability":"nonpayable","outputs":[{"type":"bool","name":"","internalType":"bool"}],"name":"_changeOwner","inputs":[{"type":"address","name":"toOwner","internalType":"address"}]},{"type":"function","stateMutability":"view","outputs":[{"type":"bytes32","name":"","internalType":"bytes32"}],"name":"getLog","inputs":[{"type":"address","name":"vendorAddress","internalType":"address"},{"type":"uint256","name":"projectId","internalType":"uint256"},{"type":"uint256","name":"logId","internalType":"uint256"}]},{"type":"function","stateMutability":"view","outputs":[{"type":"uint256","name":"","internalType":"uint256"}],"name":"numVendors","inputs":[]},{"type":"function","stateMutability":"view","outputs":[{"type":"address","name":"","internalType":"address"}],"name":"owner","inputs":[]},{"type":"function","stateMutability":"nonpayable","outputs":[{"type":"address","name":"","internalType":"address"},{"type":"uint256","name":"","internalType":"uint256"},{"type":"uint256","name":"","internalType":"uint256"},{"type":"bytes32","name":"","internalType":"bytes32"}],"name":"registerLog","inputs":[{"type":"uint256","name":"projectId","internalType":"uint256"},{"type":"bytes32","name":"data","internalType":"bytes32"}]},{"type":"function","stateMutability":"nonpayable","outputs":[{"type":"uint256","name":"","internalType":"uint256"}],"name":"registerProject","inputs":[]},{"type":"function","stateMutability":"nonpayable","outputs":[{"type":"uint256","name":"","internalType":"uint256"}],"name":"registerVendor","inputs":[]},{"type":"function","stateMutability":"view","outputs":[{"type":"address","name":"vendorAddress","internalType":"address"},{"type":"uint256","name":"projectCounter","internalType":"uint256"}],"name":"vendorLogs","inputs":[{"type":"uint256","name":"","internalType":"uint256"}]},{"type":"function","stateMutability":"view","outputs":[{"type":"address","name":"","internalType":"address"}],"name":"vendors","inputs":[{"type":"uint256","name":"","internalType":"uint256"}]},{"type":"function","stateMutability":"view","outputs":[{"type":"uint256","name":"","internalType":"uint256"}],"name":"vendorsReverse","inputs":[{"type":"address","name":"","internalType":"address"}]}]
        
```

## Installation

```
// Open Nuget package manager and search Om.Chainlogger or open the terminal then write this command
>> Install-Package Om.Chainlogger -Version 1.0.3
// .Net.CLI download package command
>> dotnet add package Om.Chainlogger --version 1.0.3
// PackageReference download package command
>> <PackageReference Include="Om.Chainlogger" Version="1.0.3" />
```

After installing or downloading, you can include the Chain Logger on your projects as following

```csharp
using Om.Chainlogger;

var logger = new Logger();
logger.SetProvider('YOUR_PROVIDER_URL');
logger.SetAbi('CONTRACT_ABI');
logger.SetEthSigner('YOUR_ETH_SIGNER_PROVIDER');
logger.SetWeb3();
logger.SetSalt("MY_SECRET_SALT");
logger.SetContract('CONTRACT_ADDRESS');
logger.SetSendContract('CONTRACT_ADDRESS');
logger.SetAccount('YOUR_WALLET_ADDRESS');
```

## Registering vendor

```csharp
await this.logger.RegisterVendorAsync()

#Returns the txHash of the call
```

## Registering project

```csharp
await this.logger.RegisterProjectAsync()

#Returns the txHash of the call
```

## Registering a log

```csharp
await this.logger.RegisterLogAsync(projectId, rawInput);

#Returns the txHash of the call
```

## Getting tx receipt for registerLog method

```csharp
await this.logger.GetTransactionReceiptAsync(txId);
```

## Verifying data from blockchain

```csharp
this.logger.VerifyData(hashedDataFromBlockchain, rawInput, salt)

#Returns boolean
```
