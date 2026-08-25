namespace WMS.API.Constants;

public static class ApiRoutes
{
    private const string Api = "api";
    private const string Version = "v1";
    private const string Base = $"{Api}/{Version}";
    private const string AdminBase = $"{Base}/admin";

    public static class Admin
    {
        public const string Auth = $"{AdminBase}/auth";
        public const string Files = $"{AdminBase}/files";
        public const string Contractors = $"{AdminBase}/contractors";
        public const string ContractorCategories = $"{AdminBase}/contractor-categories";
        public const string Labels = $"{AdminBase}/labels";
        public const string Attachments = $"{AdminBase}/attachments";
        public const string ContractTypes = $"{AdminBase}/contract-types";
        public const string Contracts = $"{AdminBase}/contracts";
        public const string ContractCategories = $"{AdminBase}/contract-categories";
        public const string ContractSteps = $"{AdminBase}/contract-steps";

        public const string AttachmentTypes = $"{AdminBase}/attachment-types";
        public const string EntityAttachments = $"{AdminBase}/entity-attachments";
        public const string ContractTypeSteps = $"{AdminBase}/contract-type-steps";
        public const string ContractTypeStates = $"{AdminBase}/contract-type-states";
        public const string ExtraOrDeductionTypes = $"{AdminBase}/extra-or-deduction-types";
        public const string ExtraOrDeductionRules = $"{AdminBase}/extra-or-deduction-rules";
        public const string ContractorStatements = $"{AdminBase}/contractor-statements";
        public const string DeadlineAlarm = $"{AdminBase}/DeadlineAlarm";



    }

    public static class Generic
    {
        public const string Auth = $"{Base}/auth";
        public const string Files = $"{Base}/files";
        public const string Locations = $"{Base}/locations";
        public const string Users = $"{Base}/users";
    }
}
