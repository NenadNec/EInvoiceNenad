// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;
using eInvoice.Core.Models;
using static eInvoice.Core.Models.Enums;

public class InvoiceDto
{
    public int? ReceiverId { get; set; }
    public SspCustomerSupplierDto PublicPurchaseContractSigner { get; set; }
    public int? PublicPurchaseContractSignerContractId { get; set; }
    public SspContractDto Contract { get; set; }
    public SalesInvoiceStatus Status { get; set; }
    public CompanyDto? UasSender { get; set; }
    public int TotalRowsCount { get; set; }
    public int? Channel { get; set; }
    public string? ChannelAddress { get; set; }
    public string? ServiceProvider { get; set; }
    public List<LineItemDto>? Rows { get; set; }
    public string? InvoiceMessage { get; set; }
    public string? AcceptRejectMessage { get; set; }
    public List<SalesInvoiceAttachmentDto> Attachments { get; set; }
    public List<SalesInvoiceBankAccountDto> BankAccounts { get; set; }
    public List<SelectedIndividualPrepaymentInvoiceDto> SelectedPrepaymentInvoices { get; set; }
    public bool? IsCreditInvoice { get; set; }
    public string SenderReceiverContractNumber { get; set; }
    public ErrorCode? ErrorCode { get; set; } 
    public DateTime? BalanceDateUtc { get; set; }
    public double? BalanceBeginSum { get; set; }
    public double? BalanceInboundSum { get; set; }
    public double? BalanceOutboundSum { get; set; }
    public double? BalanceEndSum { get; set; }
    public double? TotalToPay { get; set; }
    public bool? SendInvoiceToCir { get; set; }
    public string CirInvoiceId { get; set; }
    public string CirAmountChangeId { get; set; }
    public double CirSettledAmount { get; set; }
    public bool? IsProFormaInvoice { get; set; }
    public CirHistoryDto CirHistory { get; set; }
    public InvoiceHistoryDto CirAssignationHistory { get; set; }
    public CirInvoiceStatus CirStatus { get; set; }
    public bool IsDebitNote { get; set; }
    public string? StornoNumber { get; set; }
    public string? CancelInvoiceMessage { get; set; }
    public string? PrepaymentInvoiceNumber { get; set; }
    public bool IsPrepaymentInvoice { get; set; }
    public bool VatNotCalculated { get; set; }
    public int? VatExemptionReasonId { get; set; }
    public string? VatExemptionReasonKey { get; set; }
    public string? VatExemtionFreeFormNote { get; set; }
    public int VatPointDate { get; set; }
    public string? VatCategoryCode { get; set; }
    public string? GlobUniqId { get; set; }
    public string? VatNumberFactoringCompany { get; set; }
    public string? FactoringContractNumber { get; set; }
    public SourceInvoiceSelectionMode? SourceInvoiceSelectionMode { get; set; }
    public DateTime? IndebtednessPeriodFromDate { get; set; }
    public DateTime? IndebtednessPeriodToDate { get; set; }
    public List<InvoiceLinkDto> SourceInvoices { get; set; }
    public List<InvoiceLinkDto> CreditInvoices { get; set; }
    public List<InvoiceLinkDto> DebitNotes { get; set; }
    public SalesPrepaymentCalculationDto PrepaymentCalculation { get; set; }
    public SalesInvoiceTotalPaymentsCalculationDto InvoiceTotalPaymentsCalculation { get; set; }
    public string? ServiceId { get; set; }
    public int InvoiceId { get; set; }
    public int SenderId { get; set; }
    public string? Sender { get; set; }
    public string? Receiver { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime? AccountingDateUtc { get; set; }
    public DateTime? PaymentDateUtc { get; set; }
    public DateTime? InvoiceDateUtc { get; set; }
    public DateTime? InvoiceSentDateUtc { get; set; }
    public string? ReferenceNumber { get; set; }
    public double FineRatePerDay { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public string? OrderNumber { get; set; }
    public string? Currency { get; set; }
    public double DiscountPercentage { get; set; }
    public double DiscountAmount { get; set; }
    public double SumWithoutVat { get; set; }
    public double VatRate { get; set; }
    public double VatSum { get; set; }
    public double SumWithVat { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime LastModifiedUtc { get; set; }
    public int Version { get; set; }
    public string ModelNumber { get; set; }
}

public class Address
{
    public int AddressId { get; set; }
    public string? StreetAndHouse { get; set; }
    public string? PostalIndex { get; set; }
    public string? City { get; set; }
    public int? CountryId { get; set; }
}

public class BankAccount
{
    public int BankAccountId { get; set; }
    public int? BankId { get; set; }
    public string? Currency { get; set; }
    public string? Iban { get; set; }
    public string? Swift { get; set; }
    public bool IsPrimary { get; set; }
    public string? UnifiedBankAccount { get; set; }
}

public class SspContractDto
{
    public int ContractId { get; set; }
    public SspCustomerSupplierDto CustomerSupplier { get; set; }
    public int? CustomerSupplierId { get; set; }
    public int? PaymentTerms { get; set; }
    public double? FineRatePerDay { get; set; }
    public string? ServiceId { get; set; }
    public string? ReferenceNr { get; set; }
    public string? ContactPersonName { get; set; }
    public string? ContactPersonMail { get; set; }
    public string? ContractDesc { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<ContractCustomFieldDto>? CustomFields { get; set; }
    public List<int>? Channels { get; set; }
    public bool IsDeleted { get; set; }
    public int? OwnerCompanyId { get; set; }
    public int? Channel { get; set; }
    public string? ChannelAddress { get; set; }
    public string? ServiceProvider { get; set; }
    public int? ContractNumber { get; set; }
    public string? InvoiceNotificationEmail { get; set; }
    public string? InvoiceCurrency { get; set; }
    public bool IsBudgetClient { get; set; }
    public bool SendInvoiceToCir { get; set; }
    public string? AvailableContracts { get; set; }
    public ContractApplicationDto ContractApplication { get; set; }
}

public class ContractApplicationDto
{
    public int ContractApplicationId { get; set; }
    public string? CompanyName { get; set; }
    public string? RegistrationCode { get; set; }
    public string? VatRegistrationCode { get; set; }
    public string? ServiceId { get; set; }
    public List<Address> Addresses { get; set; }
    public int? Channel { get; set; }
    public string? ChannelAddress { get; set; }
    public string? InvoiceNotificationEmail { get; set; }
    public string? ServiceProvider { get; set; }
    public int? PaymentTerms { get; set; }
    public double? FineRatePerDay { get; set; }
    public string? ReferenceNr { get; set; }
    public DateTime ContractDate { get; set; }
    public string? CompanyLabel { get; set; }
    public string? CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
    public string? ContactPersonName { get; set; }
    public string? ContactPersonEmail { get; set; }
    public int ReceiverCompanyId { get; set; }
}

public class SspCustomerSupplierDto
{
    public int CustomerSupplierId { get; set; }
    public int? OwnerCompanyId { get; set; }
    public int? CountryId { get; set; }
    public string? CompanyName { get; set; }
    public string? RegistrationCode { get; set; }
    public string? VatRegistrationCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonalId { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsCompany { get; set; }
    public string companyMail { get; set; }
    public string? Email { get; set; }
    public string Language { get; set; }
    public string? AdditionalCode { get; set; }
}

public class SspEmailDto
{
    public string Email { get; set; }
    public bool IsActivated { get; set; }
    public string? ActivationToken { get; set; }
}

public class SalesInvoiceTotalPaymentsCalculationDto
{
    public int TotalPaymentsCalculationId { get; set; }
    public int PaymentFeeWithoutVat { get; set; }
    public double TotalPayments { get; set; }
    public List<SalesInvoiceTotalPaymentsVatPerRateCalculationDto> VatPerRateCalculations { get; set; }
}
public class LineItemDto
{
    public int RowId { get; set; }
    public int InvoiceId { get; set; }
    public int OrderNo { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double DiscountPercentage { get; set; }
    public double DiscountAmount { get; set; }
    public double SumWithoutVat { get; set; }
    public double VatRate { get; set; }
    public double VatSum { get; set; }
    public double SumWithVat { get; set; }
    public bool? VatNotCalculated { get; set; }
    public string? VatCategoryCode { get; set; }
}

public class Settings
{
    public string HomeRoute { get; set; }
    public List<SspModules> Modules { get; set; }
}

public class CompanyDto
{
    public int CompanyId { get; set; }
    public int CountryId { get; set; }
    public string Name { get; set; }
    public string? WebAddress { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<BankAccount>? BankAccounts { get; set; }
    public List<SspEmailDto>? Emails { get; set; }
    public string RegistrationCode { get; set; }
    public string? VatRegistrationCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactEmail { get; set; }
    public string? Logo { get; set; }
    public Settings Settings { get; set; }
    public bool IsMainCompany { get; set; }
    public int[]? GroupId { get; set; }
    public bool IsPrivateCompany { get; set; }
    public int PackageId { get; set; }
    public string? AdditionalCode { get; set; }
    public bool? PlusChannelsActive { get; set; }
    public CompanyStatus Status { get; set; }
    public DateTime? CompanyWillBeDeletedAt { get; set; }
    public SerbiaCompanyType SerbiaCompanyType { get; set; }
    public bool NonSebIbanWarning { get; set; }
    public bool? StoreInvoiceDetails { get; set; }
    public bool? HasISP { get; set; }
    public int? InformationServiceProviderId { get; set; }
    public string? InformationServiceProviderName { get; set; }
    public bool? ISPAcceptedToRepresentCompany { get; set; }
}

public class VatPerRateCalculation
{
    public int CalculationId { get; set; }
    public int VatRate { get; set; }
    public int BaseSumForPaymentVatRate { get; set; }
    public int VatPaymentPerRate { get; set; }
}

public class SalesInvoiceAttachmentDto
{
    public int? InvoiceId { get; set; }
    public string? fileName { get; set; }
    public string? fileData { get; set; }
    public int Id { get; set; }
    public int fileSize { get; set; }
    public DateTime createdUtc { get; set; }
    public bool isUbl { get; set; }
    public bool isLink { get; set; }
    public string? link { get; set; }
   
}

public class SalesInvoiceBankAccountDto
{
    public int bankAccountId { get; set; }
    public int salesInvoiceId { get; set; }
    public string? unifiedBankAccount { get; set; }
}
public class SelectedIndividualPrepaymentInvoiceDto
{
    public int selectedPrepaymentInvoiceId { get; set; }
    public int prepaymentInvoiceId { get; set; }
    public string? prepaymentInvoiceNumber { get; set; }
    public double prepayedAmount { get; set; }
    public double prepayedVAT { get; set; }
    public string? currency { get; set; }
    public DateTime paymentDate { get; set; }
    public int contractId { get; set; }
    public DateTime? invoiceSentDateUtc { get; set; }
    public SalesIndividualPrepaymentCalculationDto prepaymentCalculation { get; set; }
}

public class SalesIndividualPrepaymentCalculationDto
{
    public int prepaymentCalculationId { get; set; }
    public double prepaymentFeeReductionsWithoutVat { get; set; }
    public double totalPrepaymentWithoutVat { get; set; }
    public double prepaymentPaidVat { get; set; }
    public double totalPrepaymentInvoice { get; set; }
    public SalesIndividualPrepaymentCalculationVatPerRateParametersDto prepaymentVatPerRateParameters { get; set; }
}
public class SalesIndividualPrepaymentCalculationVatPerRateParametersDto
{
    public int prepaymentCalculationVatPerRateParametersId { get; set; }
    public double vatRate { get; set; }
    public double prepaymentVatBaseReduction { get; set; }
    public double prepaidVatPerRate { get; set; }
}
public class InvoiceLinkDto
{
    public string invoiceId { get; set; }
    public string cirInvoiceId { get; set; }
    public string invoiceNumber { get; set; }
    public bool sentToCir { get; set; }
    public SalesInvoiceStatus status { get; set; }
}
public class SalesPrepaymentCalculationDto
{
    public int prepaymentCalculationId { get; set; }
    public double prepaymentFeeReductionsWithoutVat { get; set; }
    public double totalPrepaymentWithoutVat { get; set; }
    public double prepaymentPaidVat { get; set; }
    public double totalPrepaymentInvoice { get; set; }
    public SalesPrepaymentCalculationDtoVatPerRateParametersDto? prepaymentVatPerRateParameters { get; set; }
}

public class SalesPrepaymentCalculationDtoVatPerRateParametersDto
{
    public int prepaymentCalculationVatPerRateParametersId { get; set; }
    public double vatRate { get; set; }
    public double prepaymentVatBaseReduction { get; set; }
    public double prepaidVatPerRate { get; set; }
}
public class SalesInvoiceTotalPaymentsVatPerRateCalculationDto
{
   public int calculationId { get; set; }
   public double vatRate { get; set; }
   public double baseSumForPaymentVatRate { get; set; }
   public double vatPaymentPerRate { get; set; }

}
public class ContractCustomFieldDto
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? vaue { get; set; }
   
}



