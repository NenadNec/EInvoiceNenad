namespace eInvoice.Core.Models
{
    public class Enums
    {
        public enum CirInvoiceStatus
        {
            None, ActiveCir, InvalidCir, CancelledCir, PartiallySettled, Settled, Assigned, Proinvoice
        }

        public enum SalesInvoiceStatus
        {
            New, Draft, Sent, Paid,
            Mistake, OverDue, Archived,
            Sending, Deleted, Approved,
            Rejected, Cancelled, Storno,
            Unknown
        }

        public enum PurchaseInvoiceStatus
        {
            New, Seen, Reminded, ReNotified, Received, Deleted,
            Approved, Rejected, Cancelled, Storno, Unknown
        }

        public enum InvoiceTypes
        {
            Regular, Credit, DebitNote, Prepayment
        }
        public enum CirTicketStatus
        {
            Active,
            Canceled,
            Solved,
            UnSolved
        }

        public enum ErrorCode
        {
            Invalid, ServiceIdNotUnique, InvoiceNumberTooLong, InvoiceCurrencyInvalid, InvoiceNumberNotUnique, IbanInvalid, PasswordMissing, CurrencyIdInvalid, BankIdInvalid, CompanyCountryIdInvalid, AadressCountryIdInvalid, RequiredCompanyAddressMissing, UserCountryIdInvalid, UserNotRegistered, UserNotActivated, UserNotFound, SmartIdLoginCanceled, SmartIdTimeout, SmartIdUserNotRegistered, MobileIdNotRegistered, MobileIdCertificateError, MobileIdCanceled, MobileIdTimeout, MobileIdSimNotReady, MobileIdPhoneIsOff, MobileIdSendingError, MobileIdSimError, LoginFailed, MobileIdError, SmartIdFailed, DublicateUserPersonalId, DublicateUsername, UsernameTooShort, ExtensionInvalid, CodeInvalid, CodeDuplicate, DifferentPersonalId, UserConfirmed, ContractEmailMissing, CompanyEmailMissing, XmlInvalid, EInvoiceMissing, EInvoiceGlobalIdMissing, EInvoiceGlobalIdDublicate, EInvoiceNumberMissing, EInvoiceNumberDublicate, EInvoiceBuyerPartyMissing, EInvoiceBuyerNameMissing, EInvoiceBuyerRegNumberMissing, EInvoiceSellerPartyMissing, EInvoiceSellerNameMissing, EInvoiceSellerRegNumberMissing, EInvoiceInvoiceItemMissing, EInvoiceInvoiceItemGroupMissing, EInvoiceInvoiceItemDescriptionMissing, EInvoiceInvoiceItemDetailInfoMissing, EInvoiceInvoiceSumGroupMissing, EInvoiceInvoiceCurrencyMissing, EInvoiceInvoiceItemDetailInfoItemUnitMissing, EInvoiceInvoiceItemsCountTooBig, EInvoiceInvoiceItemsPriceTooBig, EInvoiceInvoiceItemsAmountTooBig, EInvoiceChannelIdMissing, EInvoiceChannelAddressMissing, EInvoiceChannelAddressInvalid, EInvoiceChannelAddressWithSpace, EInvoiceContractCurrencyConflict, LogoTypeIsInvalid, LogoSizeIsInvalid, LogoDimensionsAreInvalid, FileNameOrExtensionIsInvalid, UserRegistered, TokenExpired, TokenInvalidOrExpired, DublicateEmail, SerializationError, InvalidCategory, InvalidGroup, PasswordResetTokenAlreadyUsed, PasswordResetTokenIncorrect, PasswordAtLeastOneDigitRequired, PasswordShouldBeEightCharsLong, PasswordShouldBeWithUpperCase, CompanyIdMissing, IdCardInvalid, CompanyRegcodeInvalid, CompanyStatusInvalid, NoPasswordSetForThisAccount, NoTokenProvided, SmartIdRunning, MobileIdRunning, InvoicesSendingLimitReached, ExternalInvoicesSendingLimitReached, DublicateCompany, InvoiceRowsMissing, InvoiceRowDescriptionMissing, InvoiceRowUnitMissing, ReferenceNumberInvalid, InvoiceAlreadySent,
            MissingIban, CannotRejectInvoiceWithoutComment, BankAccountIncorrect, FileTooBig, InvalidCompanyId, EInvoiceSellerNameNotTheSame, EInvoiceSellerRegNumberNotTheSame, EmailMissing, CompanyNameInvalid, ChannelInvalid, BudgetCompanyDataInvalid, CurrencyInvalid, ProformaCannotBeCreditInvoice, InvoiceNumberInvalid, PackageIdInvalid, PayablePackageMissingFields, CompanyInfoMissing, PackageDowngradingOnlyByServiceDesk, OneClickOrderInsufficientRole, Be51, Be62, Be59, Be85, Be64, Be65, Be56, Be61, Be81, Be66, Be63, Be82, Be3, Be10, Be22, Be32, Be33, Be86, Be20, Be21, Be76, Be78, Be87, Be80, Be18, Be14, Be79, Be11, Be35, Be57, Be70, Be6, Be50, Be9, Be7, Be49, Be12, Be48, Be54, Be55, Be88, Be90, Bw91, Be92, Be93, Be94, Be95, Be96, Be97, Be98, Be25, Bw102, Bw103, Bw104, Bw105, Be106, ServiceIdMissing, RegCodeMissing, PersonalIdMissing, LursoftError, TooManyAttachments, StornoCommentNotDefined, CancelCommentNotDefined, InvoiceDeliveryDateMissing, InvoiceDueDateMissing, SearchDateBiggerThanYesterday, InvoiceDateLaterThanToday, PaymentDateMoreThan90DaysInFuture, IssueDateCannotBeDifferentFromTodays, CIRCompanyByJBKJSNotFound, CirCompanyNotFound, CIRMatchBUByCompanyNumber, CirInvoiceNotFound, CirInvoiceIdNotDefined, CirInvoiceNotInAssignedStatus, CirWrongCompanyTypeForRegistration, CirCompanyRegistrationResponseNotFound, CirUndefinedCompanyStatus, CirAlreadySubmitedRegistrationRequestForThisEntity, CirFailedToRetrieveValidAccountRegistrationRequests, CirError, CirUnsucessfulInvoiceRejection, CirUnsuccessfulCompanyRegistration, CirUnsuccessfulApprovementOfCompanyRegistration, CirUnsuccessfulCancellationOfCompanyRegistration, CirIdNotFound, CreateCirTicketFailed, CannotSendPrepaymentInvoiceToCir, CannotSendDebitNoteToCir, SendingPrepaymentInvoiceToCir, SendingCreditInvoiceToCir, SendingDebitNoteToCir, ContractNumberBetweenSenderAndReceiverIsMandatoryForBudgetUsers, ContractNumberBetweenSenderAndReceiverIsMandatoryForCirInvoice, EfakturaInvoiceForCIRInvoiceIdNotFound, CannotSendCreditInvoiceToCir, NegativeTotalSumCirInvoice, InvalidCaptcha, AprDataIncomplete, AprCompanyNotFound, AprMultipleCompaniesFound, SsoTokenValidationFaild, UserHasNoActiveCompanies, RequiresAALLevel2, RequiresAALLevel3, RequiresIALLevel1, RequiresIALLevel2, RequiresIALLevel3, EpaySignUpInsufficientData,
            UserNotLegalRepresentative, SignUpFailed, AprCallFailed, UserInvitationInvalid, KjsRegisterMultipleBudgetCompaniesFound, KjsRegisterCompanyNotFound, BankIsNotSupported, ErrorCallingTaxAuthority, CompanyOneClickOrderDisabled, TaxAuthorityCompanyDataNotFound, CannotBeCreditAndDebitInvoiceAtTheSameTime, CreditInvoiceMustHaveSource, DebitInvoiceMustHaveSource, SourceInvoiceNotFound, SourceInvoiceSelectionModeNotDefined, SourceInvoicesNotDefined, SourceInvoicePeriodNotDefinedCorrectly, UnknownSourceInvoiceSelectionMode, RequiredSourceInvoiceNumbersNotChosen, ErrorAmountDeltaExceedAmountTolerance, SenderCompanyEndpointIdentifierMissing, SenderEndpointSchemeIDInvalid, SenderEndpointValueEmpty, CompanyNumberLengthInvalid, SenderJBKJSLengthInvalid, SenderMissedAddingJBKJSPrefix, SenderCompanyNotFound, InvalidSenderCompany, InvalidSenderEmail, InvalidSenderContactEmail, ReceiverCompanyEndpointIdentifierMissing, ReceiverEndpointSchemeIDInvalid, ReceiverEndpointValueEmpty, ReceiverCompanyNotFound, ReceiverJBKJSLengthInvalid, ReceiverMissedAddingJBKJSPrefix, InvoiceReceiverMissing, InvoiceReceiverChannelMissing, InvoiceReceiverChannelInvalid, InvalidReceiverEmail, InvoiceNotApprovedByReceiver, UBLUnsupportedDocumentType, UBLSourceInvoiceNotFound, UBLSourceInvoiceNumberNotExist, UBLSourceInvoiceNumberNotFound, UBLSourceInvoiceNotApproved, UBLCannotBeDefinedInvoiceDocumentReferenceIDWithInvoicePeriodStartDateAndInvoicePeriodEndDateParameters, UBLNotRegularTypeOfSourceInvoice, UBLPrepaymentInvoiceNotFound, UBLNotRegularTypeOfSelectedPrepaymentInvoice, UBLMandatoryInvoiceDocumentReference, UBLUndefinedPayableAmount, UBLNotAllowedTaxAmountForRecipientCalculatesVAT, UBLNotAllowedSubtotalTaxAmountForRecipientCalculatesVAT, UBLNotAllowedPercentOfTaxableAmountForRecipientCalculatesVAT, UBLNotAllowedTaxAmountForExemptionFromVAT, UBLNotAllowedSubtotalTaxAmountForExemptionFromVAT, UBLNotAllowedPercentOfTaxableAmountForExemptionFromVAT, UBLTaxTotalNotDefined, UBLTaxAmountNotDefined, UBLTaxCategoryNotDefined, UBLTaxCategoryIdNotDefined, UBLClassifiedTaxCategoryNotDefined, UBLPercentOfTaxableAmountNotDefined, UBLTaxSubtotalNotDefined, UBLTaxSubtotalAmountlNotDefined, UBLTotalTaxAmountAndSubtotalTaxAmountDiffer, UBLTaxExemptionReasonNotInAppropriateFormat, UBLTaxExemptionReasonKeyNotDefined,
            UBLTaxExemptionReasonDecisionNumberNotDefined, UBLTaxExemptionReasonCategoryNotDefined, UBLTaxExemptionReasonCategoryNotAllowed, UBLTaxExemptionReasonCategoryNotCorrect, UBLTaxExemptionReasonLineCategoryNotCorrect, UBLErrorOccurredDuringReadingTaxTotalDetails, UBLErrorOccurredDuringReadingInvoiceLineTaxTotalDetails, UBLTaxCategoryIdUnknown, UBLJBKJSNotDefined, UBLPartyIdentificationIdIsNotInCorrectFormat, UBLJBKJSIdentificationIdMustBeDefinedOnlyOnce, UBLSenderCompanyAndSenderCompanyIdentiferDoNotMatch, UBLReceiverCompanyIdentifierMissing, UBLCannotBeDefinedPartyIdentificationIdForNonBudgetCompany, UBLFileNotFound, UBLCompanyIsNonBudgetUser, UBLCompanyIsBudgetUser, UBLCompanyWithVATRegistrationCodeIsBudgetUser, UBLVATRegistrationCodeDoesNotMatchTheRegistrationCodeOfTheCompanyWithJBKJS, UBLAttachmentObjectIsNotDefined, UBLPublicContractSignerJBKJSNotInCorrectFormat, UBLPaymentMeansNotDefined, UBLPaymentMeansCodeNotDefined, UBLPayeeFinancialAccountIdNotDefined, UBLDeliveryDateNotAllowedForThisInvoiceType, UBLTaxAmountMoreDecimalsThanPermitted, UBLTaxSubtotalTaxableAmountMoreDecimalsThanPermitted, UBLTaxSubtotalTaxAmountMoreDecimalsThanPermitted, UBLInvoiceLineTaxExtensionAmountMoreDecimalsThanPermitted, UBLInvoiceLinePriceAmountMoreDecimalsThanPermitted, UBLLineExtensionAmountMoreDecimalsThanPermitted, UBLTaxExclusiveAmountMoreDecimalsThanPermitted, UBLTaxInclusiveAmountMoreDecimalsThanPermitted, UBLAllowanceTotalAmountMoreDecimalsThanPermitted, UBLPrepayedAmountMoreDecimalsThanPermitted, UBLPayableAmountMoreDecimalsThanPermitted, VATNumberNotActive, VATRegistrationCodeLengthInvalid, VATRegistrationCodeDoesNotMatchTheRegistrationCodeOfTheCompanyWithJBKJS, VatExemptionReasonNotExists, VatExemptionFreeFormNoteNotDefined, VatExemptionReasonIdNotDefined, VatExemptionReasonKeyNotDefined, VatExemptionReasonPointOfLawNotActive, VatPointDateTypeNotAllowedForChosenDocumentType, SelectedPrepaymentInvoiceNotCorrectInvoiceId, SelectedPrepaymentInvoiceNotCorrectInvoiceNumber, SelectedPrepaymentInvoiceNotCorrectPrepayedAmount, SelectedPrepaymentInvoiceNotCorrectPrepayedVAT, SelectedPrepaymentInvoiceNotCorrectCurrency, SelectedPrepaymentInvoiceNotCorrectPaymentDate, SelectedPrepaymentInvoiceNotCorrectContractId, PrepaymentInvoicesDetailsNotDefined, PrepaymentTotalAmountCalculationNotDefined,
            PrepaymentInvoicesVatRatesDiffersFromInvoiceVatRates, InvoiceTypeCodeMissing, SourceInvoiceCannotBePrepaymentInvoiceType, SalesInvoiceNotFound, PurchaseInvoiceNotFound, InvoiceStornoCancellationDataNotDefined, InvoiceCancellationDataNotDefined, GetAprDataFromEsbFailed, NoneOfInputParametersIsDefined, SomeOfInputParametarsNotDefined, CompanyWithRegistrationNumberIsBudgetUser, CompanyWithVATRegistrationCodeIsBudgetUser, CannotGetCompanyByPassingCompanyNumberAsRegistrationNumber, AssignatorCompanyNotFound, AssignatorCompanyIsNotBudgetUser, CompanyWithJBKJSNotFound, CompanyWithRegistrationCodeNotFound, CompanyWithVATRegistrationCodeNotFound, InvoicePeriodNotDefined, InvoicePeriodDescriptionCodeNotDefined, InvoicePeriodDescriptionCodeNotProperlyDefined, UserAlreadyRegisteredToAnotherAccount, AllAttachedInvoicesToThisInvoiceMustBeCancelledFirst, InvoiceForCancellationNotFound, InvoiceForCancellationNotInSpecificStatus, InvoiceForFactoringNotFound, WrongInvoiceTypeForStorno, InvoiceCannotBeStornoCancelled, ReceiverBudgetCompanyNotActive, SenderBudgetCompanyNotActive, CompanySetAsISP, ErrorCheckingIfCompanyIsRepresentedByISP, ISPCannotRepresentCompany, InvoiceIdNotDefined, InvalidPurchaseStatus, ISPNotFound, ISPMustBeChosen, CannotDownloadLinkAttachment, DifferentSerbiaCompanyType, InvalidCompanyNumber, InvalidVatNumber, GroupVatRecordingNotFound, ISPIsNotActive, VatRecordingPublished, GroupVatRecordingAlreadySent, PublicContractSignerNotBudgetUser, PublicContractSignerNotFound, PublicContractSignerNotActive, IndividualVatRecordingNotFound, GroupIndividualVatRecordingAlreadySent, ISPIsSetForCompanyAnyOperationIsForbidden, DocumentPeriodAndSourceInvoicesCannotBeSetAtSameTime, InvoiceItemsAmountTooBig, OrderNumberOrContractNumberAreRequired, TotalToPayMoreDecimalsThanPermitted, SumWithVatMoreDecimalsThanPermitted, SumWithoutVatMoreDecimalsThanPermitted, VatSumMoreDecimalsThanPermitted, UnitPriceMoreDecimalsThanPermitted, NetAmountMoreDecimalsThanPermitted, GrossAmountMoreDecimalsThanPermitted

        }
        public enum SourceInvoiceSelectionMode
        {
            InvoiceSelection,
            PeriodSelection
        }

        public enum SspModules
        {
            root, dashboard, salesInvoices, salesInvoiceNew,
            salesInvoiceEdit, salesInvoicePreview, salesInvoiceView,
            purchases, purchaseInvoiceEdit, purchaseInvoicePublic, 
            settings, login, myDetails, usersList, userEdit, registers,
            billing, companyDetails, contactsList, invoiceSettings,
            productsList, productEdit, searchResults, conversation, 
            files, filesAdd, filesEdit, salesInvoiceUpload, companyList,
            tunnelPage, packages, contactsEdit, apiManagement, 
            salesAttachmentUpload, invoiceMessages, addCompany, 
            contractApplications, contractApplicationsUpdate, salesInvoiceCopy,
            companyNameFromRegister, salesInvoiceCirEdit, purchaseInvoiceCirEdit,
            changePackage, oneClickOrder, companyLogo, cirTickets, cirTicketDetails,
            cirTicketNew, groupVat, groupVatEdit, singleVat, singleVatEdit, ispSettings,
            publicPurchaseContractorInvoices, ispConfiguration
        }

        public enum CompanyStatus
        {
            Active, Passive, Deleted, Migrated
        }
        public enum SerbiaCompanyType
        {
            Company, SoleProprietor, Association, BancropcyEstate, Foundation,
            SportsAssociation, Chamber, BudgetUser, Other
        }
    }
}