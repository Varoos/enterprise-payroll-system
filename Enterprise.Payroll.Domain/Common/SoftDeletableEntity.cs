namespace Enterprise.Payroll.Domain.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}