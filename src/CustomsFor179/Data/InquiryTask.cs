namespace CustomsFor179.Data
{
    /// <summary>
    /// 查询任务
    /// </summary>
    public class InquiryTask
    {
        public InquiryTask(string? sessionId, string? orderNo)
        {
            SessionId = sessionId ?? throw new ArgumentNullException(nameof(sessionId));
            OrderNo = orderNo ?? throw new ArgumentNullException(nameof(orderNo));
            TaskStatus = TaskStatus.Unhandle;
            Remark = "";
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 会话Id
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// 交易单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus TaskStatus { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 尝试失败次数
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }

        public void SetSuccess()
        {
            TaskStatus = TaskStatus.Success;
            AccessFailedCount = 0;
            Remark = "";
        }

        public void SetFail(string remark)
        {
            TaskStatus = TaskStatus.Fail;
            AccessFailedCount += 1;
            Remark = remark;
        }
    }
    public enum TaskStatus
    {
        Unhandle,
        Success,
        Fail
    }
    public class InquiryTaskEntityTypeConfiguration : IEntityTypeConfiguration<InquiryTask>
    {
        public void Configure(EntityTypeBuilder<InquiryTask> builder)
        {
            builder.ToTable("InquiryTasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasComment("Id");
            builder.Property(x => x.SessionId).HasMaxLength(36).HasComment("会话Id");
            builder.Property(x => x.OrderNo).HasMaxLength(32).HasComment("交易单号");
            builder.Property(x => x.Remark).HasMaxLength(1024).HasComment("说明");
            builder.Property(x => x.TaskStatus).HasMaxLength(1024).HasComment("任务状态");
            builder.Property(x => x.CreatedTime).HasComment("创建时间");
            builder.Property(x => x.LastModifiedTime).HasComment("最后更新时间");

            builder.HasIndex(x => new { x.SessionId, x.OrderNo }).IsUnique();

        }
    }
}
