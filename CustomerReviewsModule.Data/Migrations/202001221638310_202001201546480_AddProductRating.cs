namespace CustomerReviewsModule.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202001201546480_AddProductRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rating");
        }
    }
}
