using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace HuffPlugMiner.IHuffplug.ContractDefinition;

#nullable disable
public partial class IHuffplugDeployment : IHuffplugDeploymentBase {
	public IHuffplugDeployment() : base(BYTECODE) { }
	public IHuffplugDeployment(string byteCode) : base(byteCode) { }
}

public class IHuffplugDeploymentBase : ContractDeploymentMessage {
	public static string BYTECODE = "";
	public IHuffplugDeploymentBase() : base(BYTECODE) { }
	public IHuffplugDeploymentBase(string byteCode) : base(byteCode) { }

}

public partial class ApproveFunction : ApproveFunctionBase { }

[Function("approve")]
public class ApproveFunctionBase : FunctionMessage {
	[Parameter("address", "to", 1)]
	public virtual string To { get; set; }
	[Parameter("uint256", "tokenId", 2)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class BalanceOfFunction : BalanceOfFunctionBase { }

[Function("balanceOf", "uint256")]
public class BalanceOfFunctionBase : FunctionMessage {
	[Parameter("address", "owner", 1)]
	public virtual string Owner { get; set; }
}

public partial class ClaimedFunction : ClaimedFunctionBase { }

[Function("claimed", "bool")]
public class ClaimedFunctionBase : FunctionMessage {
	[Parameter("address", "user", 1)]
	public virtual string User { get; set; }
}

public partial class ContractURIFunction : ContractURIFunctionBase { }

[Function("contractURI", "string")]
public class ContractURIFunctionBase : FunctionMessage {

}

public partial class CurrentDifficultyFunction : CurrentDifficultyFunctionBase { }

[Function("currentDifficulty", "uint256")]
public class CurrentDifficultyFunctionBase : FunctionMessage {

}

public partial class GetApprovedFunction : GetApprovedFunctionBase { }

[Function("getApproved", "address")]
public class GetApprovedFunctionBase : FunctionMessage {
	[Parameter("uint256", "tokenId", 1)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class IsApprovedForAllFunction : IsApprovedForAllFunctionBase { }

[Function("isApprovedForAll", "bool")]
public class IsApprovedForAllFunctionBase : FunctionMessage {
	[Parameter("address", "owner", 1)]
	public virtual string Owner { get; set; }
	[Parameter("address", "operator", 2)]
	public virtual string Operator { get; set; }
}

public partial class MintFunction : MintFunctionBase { }

[Function("mint")]
public class MintFunctionBase : FunctionMessage {
	[Parameter("uint256", "nonce", 1)]
	public new virtual BigInteger Nonce { get; set; }
}

public partial class MintWithMerkleFunction : MintWithMerkleFunctionBase { }

[Function("mintWithMerkle")]
public class MintWithMerkleFunctionBase : FunctionMessage {
	[Parameter("bytes32[]", "", 1)]
	public virtual List<byte[]> ReturnValue1 { get; set; }
}

public partial class NameFunction : NameFunctionBase { }

[Function("name", "string")]
public class NameFunctionBase : FunctionMessage {

}

public partial class OwnerFunction : OwnerFunctionBase { }

[Function("owner", "address")]
public class OwnerFunctionBase : FunctionMessage {

}

public partial class OwnerOfFunction : OwnerOfFunctionBase { }

[Function("ownerOf", "address")]
public class OwnerOfFunctionBase : FunctionMessage {
	[Parameter("uint256", "tokenId", 1)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class PlugFunction : PlugFunctionBase { }

[Function("plug")]
public class PlugFunctionBase : FunctionMessage {
	[Parameter("address", "who", 1)]
	public virtual string Who { get; set; }
	[Parameter("uint256", "tokenid", 2)]
	public virtual BigInteger Tokenid { get; set; }
}

public partial class SafeTransferFromFunction : SafeTransferFromFunctionBase { }

[Function("safeTransferFrom")]
public class SafeTransferFromFunctionBase : FunctionMessage {
	[Parameter("address", "from", 1)]
	public virtual string From { get; set; }
	[Parameter("address", "to", 2)]
	public virtual string To { get; set; }
	[Parameter("uint256", "tokenId", 3)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class SafeTransferFrom1Function : SafeTransferFrom1FunctionBase { }

[Function("safeTransferFrom")]
public class SafeTransferFrom1FunctionBase : FunctionMessage {
	[Parameter("address", "from", 1)]
	public virtual string From { get; set; }
	[Parameter("address", "to", 2)]
	public virtual string To { get; set; }
	[Parameter("uint256", "tokenId", 3)]
	public virtual BigInteger TokenId { get; set; }
	[Parameter("bytes", "data", 4)]
	public virtual byte[] Data { get; set; }
}

public partial class SaltFunction : SaltFunctionBase { }

[Function("salt", "bytes32")]
public class SaltFunctionBase : FunctionMessage {

}

public partial class SetApprovalForAllFunction : SetApprovalForAllFunctionBase { }

[Function("setApprovalForAll")]
public class SetApprovalForAllFunctionBase : FunctionMessage {
	[Parameter("address", "operator", 1)]
	public virtual string Operator { get; set; }
	[Parameter("bool", "approved", 2)]
	public virtual bool Approved { get; set; }
}

public partial class SetContractUriFunction : SetContractUriFunctionBase { }

[Function("setContractUri")]
public class SetContractUriFunctionBase : FunctionMessage {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class SetOwnerFunction : SetOwnerFunctionBase { }

[Function("setOwner")]
public class SetOwnerFunctionBase : FunctionMessage {
	[Parameter("address", "new_owner", 1)]
	public virtual string NewOwner { get; set; }
}

public partial class SetUriFunction : SetUriFunctionBase { }

[Function("setUri")]
public class SetUriFunctionBase : FunctionMessage {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

[Function("supportsInterface", "bool")]
public class SupportsInterfaceFunctionBase : FunctionMessage {
	[Parameter("bytes4", "interfaceId", 1)]
	public virtual byte[] InterfaceId { get; set; }
}

public partial class SymbolFunction : SymbolFunctionBase { }

[Function("symbol", "string")]
public class SymbolFunctionBase : FunctionMessage {

}

public partial class TokenURIFunction : TokenURIFunctionBase { }

[Function("tokenURI", "string")]
public class TokenURIFunctionBase : FunctionMessage {
	[Parameter("uint256", "_tokenId", 1)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class TotalMintedFunction : TotalMintedFunctionBase { }

[Function("totalMinted", "uint256")]
public class TotalMintedFunctionBase : FunctionMessage {

}

public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

[Function("totalSupply", "uint256")]
public class TotalSupplyFunctionBase : FunctionMessage {

}

public partial class TransferFromFunction : TransferFromFunctionBase { }

[Function("transferFrom")]
public class TransferFromFunctionBase : FunctionMessage {
	[Parameter("address", "from", 1)]
	public virtual string From { get; set; }
	[Parameter("address", "to", 2)]
	public virtual string To { get; set; }
	[Parameter("uint256", "tokenId", 3)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

[Event("Approval")]
public class ApprovalEventDTOBase : IEventDTO {
	[Parameter("address", "owner", 1, true)]
	public virtual string Owner { get; set; }
	[Parameter("address", "approved", 2, true)]
	public virtual string Approved { get; set; }
	[Parameter("uint256", "tokenId", 3, true)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class ApprovalForAllEventDTO : ApprovalForAllEventDTOBase { }

[Event("ApprovalForAll")]
public class ApprovalForAllEventDTOBase : IEventDTO {
	[Parameter("address", "owner", 1, true)]
	public virtual string Owner { get; set; }
	[Parameter("address", "operator", 2, true)]
	public virtual string Operator { get; set; }
	[Parameter("bool", "approved", 3, false)]
	public virtual bool Approved { get; set; }
}

public partial class OwnerUpdatedEventDTO : OwnerUpdatedEventDTOBase { }

[Event("OwnerUpdated")]
public class OwnerUpdatedEventDTOBase : IEventDTO {
	[Parameter("address", "user", 1, true)]
	public virtual string User { get; set; }
	[Parameter("address", "newOwner", 2, true)]
	public virtual string NewOwner { get; set; }
}

public partial class TransferEventDTO : TransferEventDTOBase { }

[Event("Transfer")]
public class TransferEventDTOBase : IEventDTO {
	[Parameter("address", "from", 1, true)]
	public virtual string From { get; set; }
	[Parameter("address", "to", 2, true)]
	public virtual string To { get; set; }
	[Parameter("uint256", "tokenId", 3, true)]
	public virtual BigInteger TokenId { get; set; }
}

public partial class ErrAlreadyClaimedError : ErrAlreadyClaimedErrorBase { }
[Error("ErrAlreadyClaimed")]
public class ErrAlreadyClaimedErrorBase : IErrorDTO {
}

public partial class ErrInvalidProofError : ErrInvalidProofErrorBase { }
[Error("ErrInvalidProof")]
public class ErrInvalidProofErrorBase : IErrorDTO {
}

public partial class ErrNoMoreUwUError : ErrNoMoreUwUErrorBase { }
[Error("ErrNoMoreUwU")]
public class ErrNoMoreUwUErrorBase : IErrorDTO {
}

public partial class ErrOnlyOwnerError : ErrOnlyOwnerErrorBase { }
[Error("ErrOnlyOwner")]
public class ErrOnlyOwnerErrorBase : IErrorDTO {
}

public partial class ErrWrongSaltError : ErrWrongSaltErrorBase { }
[Error("ErrWrongSalt")]
public class ErrWrongSaltErrorBase : IErrorDTO {
}



public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

[FunctionOutput]
public class BalanceOfOutputDTOBase : IFunctionOutputDTO {
	[Parameter("uint256", "balance", 1)]
	public virtual BigInteger Balance { get; set; }
}

public partial class ClaimedOutputDTO : ClaimedOutputDTOBase { }

[FunctionOutput]
public class ClaimedOutputDTOBase : IFunctionOutputDTO {
	[Parameter("bool", "", 1)]
	public virtual bool ReturnValue1 { get; set; }
}

public partial class ContractURIOutputDTO : ContractURIOutputDTOBase { }

[FunctionOutput]
public class ContractURIOutputDTOBase : IFunctionOutputDTO {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class CurrentDifficultyOutputDTO : CurrentDifficultyOutputDTOBase { }

[FunctionOutput]
public class CurrentDifficultyOutputDTOBase : IFunctionOutputDTO {
	[Parameter("uint256", "", 1)]
	public virtual BigInteger ReturnValue1 { get; set; }
}

public partial class GetApprovedOutputDTO : GetApprovedOutputDTOBase { }

[FunctionOutput]
public class GetApprovedOutputDTOBase : IFunctionOutputDTO {
	[Parameter("address", "operator", 1)]
	public virtual string Operator { get; set; }
}

public partial class IsApprovedForAllOutputDTO : IsApprovedForAllOutputDTOBase { }

[FunctionOutput]
public class IsApprovedForAllOutputDTOBase : IFunctionOutputDTO {
	[Parameter("bool", "", 1)]
	public virtual bool ReturnValue1 { get; set; }
}





public partial class NameOutputDTO : NameOutputDTOBase { }

[FunctionOutput]
public class NameOutputDTOBase : IFunctionOutputDTO {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

[FunctionOutput]
public class OwnerOutputDTOBase : IFunctionOutputDTO {
	[Parameter("address", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class OwnerOfOutputDTO : OwnerOfOutputDTOBase { }

[FunctionOutput]
public class OwnerOfOutputDTOBase : IFunctionOutputDTO {
	[Parameter("address", "owner", 1)]
	public virtual string Owner { get; set; }
}







public partial class SaltOutputDTO : SaltOutputDTOBase { }

[FunctionOutput]
public class SaltOutputDTOBase : IFunctionOutputDTO {
	[Parameter("bytes32", "", 1)]
	public virtual byte[] ReturnValue1 { get; set; }
}









public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

[FunctionOutput]
public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO {
	[Parameter("bool", "", 1)]
	public virtual bool ReturnValue1 { get; set; }
}

public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

[FunctionOutput]
public class SymbolOutputDTOBase : IFunctionOutputDTO {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class TokenURIOutputDTO : TokenURIOutputDTOBase { }

[FunctionOutput]
public class TokenURIOutputDTOBase : IFunctionOutputDTO {
	[Parameter("string", "", 1)]
	public virtual string ReturnValue1 { get; set; }
}

public partial class TotalMintedOutputDTO : TotalMintedOutputDTOBase { }

[FunctionOutput]
public class TotalMintedOutputDTOBase : IFunctionOutputDTO {
	[Parameter("uint256", "", 1)]
	public virtual BigInteger ReturnValue1 { get; set; }
}

public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

[FunctionOutput]
public class TotalSupplyOutputDTOBase : IFunctionOutputDTO {
	[Parameter("uint256", "", 1)]
	public virtual BigInteger ReturnValue1 { get; set; }
}
