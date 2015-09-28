<Query Kind="Program" />

void Main()
{
	
}

public class ProductRefModel
    {
        [TApiRequiredAttribute]
        public string LongName { get; set; }

        [TApiRequiredAttribute]
        public List<Int64> Categories { get; set; }

        [TApiRequiredAttribute]
        public List<ContentModel> NetContents { get; set; }

        [TApiRequiredAttribute]
        public Guid Packaging { get; set; }

        [TApiRequiredAttribute]
        public List<CodeModel> Codes { get; set; }

        [EnsureValidCompanyIdAttribute]
        public Int64 CompanyId { get; set; }

        [TApiRequiredAttribute]
        public Guid ProductType { get; set; }
    }

	
    public class ContentModel
	{
		[EnsureValidNetContentAttribute]
        public decimal? Value { get; set; }

		[TApiRequiredAttribute]
        public Guid Unit { get; set; }
    }
	
    public class CodeModel
    {
        public string Value { get; set; }
		
        public Guid Type { get; set; }
    }
