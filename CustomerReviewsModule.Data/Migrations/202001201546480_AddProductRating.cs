namespace CustomerReviewsModule.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddProductRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Rating", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rating");
        }
    }
}
