using HuffPlugMiner.IHuffplug.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System.Numerics;

namespace HuffPlugMiner.IHuffplug;

#nullable disable
public partial class IHuffplugService {
	public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(IWeb3 web3, IHuffplugDeployment iHuffplugDeployment, CancellationTokenSource cancellationTokenSource = null) {
		return web3.Eth.GetContractDeploymentHandler<IHuffplugDeployment>().SendRequestAndWaitForReceiptAsync(iHuffplugDeployment, cancellationTokenSource);
	}

	public static Task<string> DeployContractAsync(IWeb3 web3, IHuffplugDeployment iHuffplugDeployment) {
		return web3.Eth.GetContractDeploymentHandler<IHuffplugDeployment>().SendRequestAsync(iHuffplugDeployment);
	}

	public static async Task<IHuffplugService> DeployContractAndGetServiceAsync(IWeb3 web3, IHuffplugDeployment iHuffplugDeployment, CancellationTokenSource cancellationTokenSource = null) {
		var receipt = await DeployContractAndWaitForReceiptAsync(web3, iHuffplugDeployment, cancellationTokenSource);
		return new IHuffplugService(web3, receipt.ContractAddress);
	}

	protected IWeb3 Web3 { get; }

	public ContractHandler ContractHandler { get; }

	public IHuffplugService(Web3 web3, string contractAddress) {
		Web3 = web3;
		ContractHandler = web3.Eth.GetContractHandler(contractAddress);
	}

	public IHuffplugService(IWeb3 web3, string contractAddress) {
		Web3 = web3;
		ContractHandler = web3.Eth.GetContractHandler(contractAddress);
	}

	public Task<string> ApproveRequestAsync(ApproveFunction approveFunction) {
		return ContractHandler.SendRequestAsync(approveFunction);
	}

	public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
	}

	public Task<string> ApproveRequestAsync(string to, BigInteger tokenId) {
		var approveFunction = new ApproveFunction();
		approveFunction.To = to;
		approveFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(approveFunction);
	}

	public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null) {
		var approveFunction = new ApproveFunction();
		approveFunction.To = to;
		approveFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
	}

	public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
	}


	public Task<BigInteger> BalanceOfQueryAsync(string owner, BlockParameter blockParameter = null) {
		var balanceOfFunction = new BalanceOfFunction();
		balanceOfFunction.Owner = owner;

		return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
	}

	public Task<bool> ClaimedQueryAsync(ClaimedFunction claimedFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<ClaimedFunction, bool>(claimedFunction, blockParameter);
	}


	public Task<bool> ClaimedQueryAsync(string user, BlockParameter blockParameter = null) {
		var claimedFunction = new ClaimedFunction();
		claimedFunction.User = user;

		return ContractHandler.QueryAsync<ClaimedFunction, bool>(claimedFunction, blockParameter);
	}

	public Task<string> ContractURIQueryAsync(ContractURIFunction contractURIFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<ContractURIFunction, string>(contractURIFunction, blockParameter);
	}


	public Task<string> ContractURIQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<ContractURIFunction, string>(null, blockParameter);
	}

	public Task<BigInteger> CurrentDifficultyQueryAsync(CurrentDifficultyFunction currentDifficultyFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<CurrentDifficultyFunction, BigInteger>(currentDifficultyFunction, blockParameter);
	}


	public Task<BigInteger> CurrentDifficultyQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<CurrentDifficultyFunction, BigInteger>(null, blockParameter);
	}

	public Task<string> GetApprovedQueryAsync(GetApprovedFunction getApprovedFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<GetApprovedFunction, string>(getApprovedFunction, blockParameter);
	}


	public Task<string> GetApprovedQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null) {
		var getApprovedFunction = new GetApprovedFunction();
		getApprovedFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<GetApprovedFunction, string>(getApprovedFunction, blockParameter);
	}

	public Task<bool> IsApprovedForAllQueryAsync(IsApprovedForAllFunction isApprovedForAllFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<IsApprovedForAllFunction, bool>(isApprovedForAllFunction, blockParameter);
	}


	public Task<bool> IsApprovedForAllQueryAsync(string owner, string @operator, BlockParameter blockParameter = null) {
		var isApprovedForAllFunction = new IsApprovedForAllFunction();
		isApprovedForAllFunction.Owner = owner;
		isApprovedForAllFunction.Operator = @operator;

		return ContractHandler.QueryAsync<IsApprovedForAllFunction, bool>(isApprovedForAllFunction, blockParameter);
	}

	public Task<string> MintRequestAsync(MintFunction mintFunction) {
		return ContractHandler.SendRequestAsync(mintFunction);
	}

	public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(MintFunction mintFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
	}

	public Task<string> MintRequestAsync(BigInteger nonce) {
		var mintFunction = new MintFunction();
		mintFunction.Nonce = nonce;

		return ContractHandler.SendRequestAsync(mintFunction);
	}

	public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(BigInteger nonce, CancellationTokenSource cancellationToken = null) {
		var mintFunction = new MintFunction();
		mintFunction.Nonce = nonce;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
	}

	public Task<string> MintWithMerkleRequestAsync(MintWithMerkleFunction mintWithMerkleFunction) {
		return ContractHandler.SendRequestAsync(mintWithMerkleFunction);
	}

	public Task<TransactionReceipt> MintWithMerkleRequestAndWaitForReceiptAsync(MintWithMerkleFunction mintWithMerkleFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(mintWithMerkleFunction, cancellationToken);
	}

	public Task<string> MintWithMerkleRequestAsync(List<byte[]> returnValue1) {
		var mintWithMerkleFunction = new MintWithMerkleFunction();
		mintWithMerkleFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAsync(mintWithMerkleFunction);
	}

	public Task<TransactionReceipt> MintWithMerkleRequestAndWaitForReceiptAsync(List<byte[]> returnValue1, CancellationTokenSource cancellationToken = null) {
		var mintWithMerkleFunction = new MintWithMerkleFunction();
		mintWithMerkleFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(mintWithMerkleFunction, cancellationToken);
	}

	public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
	}


	public Task<string> NameQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
	}

	public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
	}


	public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
	}

	public Task<string> OwnerOfQueryAsync(OwnerOfFunction ownerOfFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction, blockParameter);
	}


	public Task<string> OwnerOfQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null) {
		var ownerOfFunction = new OwnerOfFunction();
		ownerOfFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction, blockParameter);
	}

	public Task<string> PlugRequestAsync(PlugFunction plugFunction) {
		return ContractHandler.SendRequestAsync(plugFunction);
	}

	public Task<TransactionReceipt> PlugRequestAndWaitForReceiptAsync(PlugFunction plugFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(plugFunction, cancellationToken);
	}

	public Task<string> PlugRequestAsync(string who, BigInteger tokenid) {
		var plugFunction = new PlugFunction();
		plugFunction.Who = who;
		plugFunction.Tokenid = tokenid;

		return ContractHandler.SendRequestAsync(plugFunction);
	}

	public Task<TransactionReceipt> PlugRequestAndWaitForReceiptAsync(string who, BigInteger tokenid, CancellationTokenSource cancellationToken = null) {
		var plugFunction = new PlugFunction();
		plugFunction.Who = who;
		plugFunction.Tokenid = tokenid;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(plugFunction, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(SafeTransferFromFunction safeTransferFromFunction) {
		return ContractHandler.SendRequestAsync(safeTransferFromFunction);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(SafeTransferFromFunction safeTransferFromFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFromFunction, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(string from, string to, BigInteger tokenId) {
		var safeTransferFromFunction = new SafeTransferFromFunction();
		safeTransferFromFunction.From = from;
		safeTransferFromFunction.To = to;
		safeTransferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(safeTransferFromFunction);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null) {
		var safeTransferFromFunction = new SafeTransferFromFunction();
		safeTransferFromFunction.From = from;
		safeTransferFromFunction.To = to;
		safeTransferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFromFunction, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(SafeTransferFrom1Function safeTransferFrom1Function) {
		return ContractHandler.SendRequestAsync(safeTransferFrom1Function);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(SafeTransferFrom1Function safeTransferFrom1Function, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFrom1Function, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(string from, string to, BigInteger tokenId, byte[] data) {
		var safeTransferFrom1Function = new SafeTransferFrom1Function();
		safeTransferFrom1Function.From = from;
		safeTransferFrom1Function.To = to;
		safeTransferFrom1Function.TokenId = tokenId;
		safeTransferFrom1Function.Data = data;

		return ContractHandler.SendRequestAsync(safeTransferFrom1Function);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, byte[] data, CancellationTokenSource cancellationToken = null) {
		var safeTransferFrom1Function = new SafeTransferFrom1Function();
		safeTransferFrom1Function.From = from;
		safeTransferFrom1Function.To = to;
		safeTransferFrom1Function.TokenId = tokenId;
		safeTransferFrom1Function.Data = data;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFrom1Function, cancellationToken);
	}

	public Task<byte[]> SaltQueryAsync(SaltFunction saltFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<SaltFunction, byte[]>(saltFunction, blockParameter);
	}


	public Task<byte[]> SaltQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<SaltFunction, byte[]>(null, blockParameter);
	}

	public Task<string> SetApprovalForAllRequestAsync(SetApprovalForAllFunction setApprovalForAllFunction) {
		return ContractHandler.SendRequestAsync(setApprovalForAllFunction);
	}

	public Task<TransactionReceipt> SetApprovalForAllRequestAndWaitForReceiptAsync(SetApprovalForAllFunction setApprovalForAllFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(setApprovalForAllFunction, cancellationToken);
	}

	public Task<string> SetApprovalForAllRequestAsync(string @operator, bool approved) {
		var setApprovalForAllFunction = new SetApprovalForAllFunction();
		setApprovalForAllFunction.Operator = @operator;
		setApprovalForAllFunction.Approved = approved;

		return ContractHandler.SendRequestAsync(setApprovalForAllFunction);
	}

	public Task<TransactionReceipt> SetApprovalForAllRequestAndWaitForReceiptAsync(string @operator, bool approved, CancellationTokenSource cancellationToken = null) {
		var setApprovalForAllFunction = new SetApprovalForAllFunction();
		setApprovalForAllFunction.Operator = @operator;
		setApprovalForAllFunction.Approved = approved;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(setApprovalForAllFunction, cancellationToken);
	}

	public Task<string> SetContractUriRequestAsync(SetContractUriFunction setContractUriFunction) {
		return ContractHandler.SendRequestAsync(setContractUriFunction);
	}

	public Task<TransactionReceipt> SetContractUriRequestAndWaitForReceiptAsync(SetContractUriFunction setContractUriFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(setContractUriFunction, cancellationToken);
	}

	public Task<string> SetContractUriRequestAsync(string returnValue1) {
		var setContractUriFunction = new SetContractUriFunction();
		setContractUriFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAsync(setContractUriFunction);
	}

	public Task<TransactionReceipt> SetContractUriRequestAndWaitForReceiptAsync(string returnValue1, CancellationTokenSource cancellationToken = null) {
		var setContractUriFunction = new SetContractUriFunction();
		setContractUriFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(setContractUriFunction, cancellationToken);
	}

	public Task<string> SetOwnerRequestAsync(SetOwnerFunction setOwnerFunction) {
		return ContractHandler.SendRequestAsync(setOwnerFunction);
	}

	public Task<TransactionReceipt> SetOwnerRequestAndWaitForReceiptAsync(SetOwnerFunction setOwnerFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(setOwnerFunction, cancellationToken);
	}

	public Task<string> SetOwnerRequestAsync(string newOwner) {
		var setOwnerFunction = new SetOwnerFunction();
		setOwnerFunction.NewOwner = newOwner;

		return ContractHandler.SendRequestAsync(setOwnerFunction);
	}

	public Task<TransactionReceipt> SetOwnerRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null) {
		var setOwnerFunction = new SetOwnerFunction();
		setOwnerFunction.NewOwner = newOwner;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(setOwnerFunction, cancellationToken);
	}

	public Task<string> SetUriRequestAsync(SetUriFunction setUriFunction) {
		return ContractHandler.SendRequestAsync(setUriFunction);
	}

	public Task<TransactionReceipt> SetUriRequestAndWaitForReceiptAsync(SetUriFunction setUriFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(setUriFunction, cancellationToken);
	}

	public Task<string> SetUriRequestAsync(string returnValue1) {
		var setUriFunction = new SetUriFunction();
		setUriFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAsync(setUriFunction);
	}

	public Task<TransactionReceipt> SetUriRequestAndWaitForReceiptAsync(string returnValue1, CancellationTokenSource cancellationToken = null) {
		var setUriFunction = new SetUriFunction();
		setUriFunction.ReturnValue1 = returnValue1;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(setUriFunction, cancellationToken);
	}

	public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
	}


	public Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null) {
		var supportsInterfaceFunction = new SupportsInterfaceFunction();
		supportsInterfaceFunction.InterfaceId = interfaceId;

		return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
	}

	public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
	}


	public Task<string> SymbolQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
	}

	public Task<string> TokenURIQueryAsync(TokenURIFunction tokenURIFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<TokenURIFunction, string>(tokenURIFunction, blockParameter);
	}


	public Task<string> TokenURIQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null) {
		var tokenURIFunction = new TokenURIFunction();
		tokenURIFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<TokenURIFunction, string>(tokenURIFunction, blockParameter);
	}

	public Task<BigInteger> TotalMintedQueryAsync(TotalMintedFunction totalMintedFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<TotalMintedFunction, BigInteger>(totalMintedFunction, blockParameter);
	}


	public Task<BigInteger> TotalMintedQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<TotalMintedFunction, BigInteger>(null, blockParameter);
	}

	public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
	}


	public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null) {
		return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
	}

	public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction) {
		return ContractHandler.SendRequestAsync(transferFromFunction);
	}

	public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null) {
		return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
	}

	public Task<string> TransferFromRequestAsync(string from, string to, BigInteger tokenId) {
		var transferFromFunction = new TransferFromFunction();
		transferFromFunction.From = from;
		transferFromFunction.To = to;
		transferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(transferFromFunction);
	}

	public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null) {
		var transferFromFunction = new TransferFromFunction();
		transferFromFunction.From = from;
		transferFromFunction.To = to;
		transferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
	}
}
