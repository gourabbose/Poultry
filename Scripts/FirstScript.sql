CREATE TABLE [dbo].[UserProfile] (
    [UserId] [int] NOT NULL IDENTITY,
    [UserName] [nvarchar](max),
    CONSTRAINT [PK_dbo.UserProfile] PRIMARY KEY ([UserId])
)
CREATE TABLE [dbo].[Vendors] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Address] [nvarchar](max) NOT NULL,
    [ContactNo] [nvarchar](max) NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [Due] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Vendors] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Farmers] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Address] [nvarchar](max) NOT NULL,
    [ContactNo] [nvarchar](max) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Farmers] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Items] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Type] [int] NOT NULL,
    [Unit] [nvarchar](max) NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Items] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Stocks] (
    [Id] [int] NOT NULL IDENTITY,
    [Quantity] [int] NOT NULL,
    [Item_Id] [int],
    CONSTRAINT [PK_dbo.Stocks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Item_Id] ON [dbo].[Stocks]([Item_Id])
CREATE TABLE [dbo].[VendorLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Payment] [int] NOT NULL,
    [PaymentMode] [nvarchar](max),
    [FolioNo] [nvarchar](max),
    [Vendor_Id] [int],
    CONSTRAINT [PK_dbo.VendorLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Vendor_Id] ON [dbo].[VendorLogs]([Vendor_Id])
CREATE TABLE [dbo].[ItemTransactions] (
    [Id] [int] NOT NULL IDENTITY,
    [Qty] [int] NOT NULL,
    [Price] [int] NOT NULL,
    [Item_Id] [int],
    [VendorLog_Id] [int],
    [FarmerLog_Id] [int],
    CONSTRAINT [PK_dbo.ItemTransactions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Item_Id] ON [dbo].[ItemTransactions]([Item_Id])
CREATE INDEX [IX_VendorLog_Id] ON [dbo].[ItemTransactions]([VendorLog_Id])
CREATE INDEX [IX_FarmerLog_Id] ON [dbo].[ItemTransactions]([FarmerLog_Id])
CREATE TABLE [dbo].[FarmerLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Payment] [int] NOT NULL,
    [PaymentMethod] [nvarchar](max),
    [ActivityFlag] [bit] NOT NULL,
    [Lifted] [int] NOT NULL,
    [TotalDeath] [int] NOT NULL,
    [Farmer_Id] [int],
    CONSTRAINT [PK_dbo.FarmerLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Farmer_Id] ON [dbo].[FarmerLogs]([Farmer_Id])
CREATE TABLE [dbo].[TraderLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [ChickenCount] [int] NOT NULL,
    [Price] [int] NOT NULL,
    [Payment] [int] NOT NULL,
    [PaymentMethod] [nvarchar](max),
    [Trader_Id] [int],
    CONSTRAINT [PK_dbo.TraderLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Trader_Id] ON [dbo].[TraderLogs]([Trader_Id])
CREATE TABLE [dbo].[Traders] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Address] [nvarchar](max) NOT NULL,
    [ContactNo] [nvarchar](max) NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [PaymentDue] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Traders] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ConsumptionLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Quantity] [int] NOT NULL,
    [Ratio] [int] NOT NULL,
    [For_Id] [int],
    [Item_Id] [int],
    CONSTRAINT [PK_dbo.ConsumptionLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_For_Id] ON [dbo].[ConsumptionLogs]([For_Id])
CREATE INDEX [IX_Item_Id] ON [dbo].[ConsumptionLogs]([Item_Id])
CREATE TABLE [dbo].[CostSheets] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [RatePerKg] [int] NOT NULL,
    [BPS_Qty] [int] NOT NULL,
    [BPS_Amt] [int] NOT NULL,
    [BS_Qty] [int] NOT NULL,
    [BS_Amt] [int] NOT NULL,
    [BF_Qty] [int] NOT NULL,
    [BF_Amt] [int] NOT NULL,
    [Remarks] [nvarchar](max),
    [Item_Id] [int],
    CONSTRAINT [PK_dbo.CostSheets] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Item_Id] ON [dbo].[CostSheets]([Item_Id])
CREATE TABLE [dbo].[VendorPaymentLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Amount] [int] NOT NULL,
    [Vendor_Id] [int],
    CONSTRAINT [PK_dbo.VendorPaymentLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Vendor_Id] ON [dbo].[VendorPaymentLogs]([Vendor_Id])
CREATE TABLE [dbo].[ProductionLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Qty] [int] NOT NULL,
    [Item_Id] [int],
    CONSTRAINT [PK_dbo.ProductionLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Item_Id] ON [dbo].[ProductionLogs]([Item_Id])
CREATE TABLE [dbo].[WeeklyReports] (
    [Id] [int] NOT NULL IDENTITY,
    [WeekNo] [int] NOT NULL,
    [AvgWt] [int] NOT NULL,
    [Remarks] [nvarchar](max),
    [FCR] [nvarchar](max),
    [Day1_ChickCount] [int] NOT NULL,
    [Day1_DeathCountPerDay] [int] NOT NULL,
    [Day1_DeathRemarks] [nvarchar](max),
    [Day1_TotalDeathCount] [int] NOT NULL,
    [Day1_FoodConsumption] [real] NOT NULL,
    [Day1_AvgConsumption] [real] NOT NULL,
    [Day1_Remarks] [nvarchar](max),
    [Day1_InStock] [nvarchar](max),
    [Day1_CurrentWt] [real] NOT NULL,
    [Day1_FCR] [nvarchar](max),
    [Day2_ChickCount] [int] NOT NULL,
    [Day2_DeathCountPerDay] [int] NOT NULL,
    [Day2_DeathRemarks] [nvarchar](max),
    [Day2_TotalDeathCount] [int] NOT NULL,
    [Day2_FoodConsumption] [real] NOT NULL,
    [Day2_AvgConsumption] [real] NOT NULL,
    [Day2_Remarks] [nvarchar](max),
    [Day2_InStock] [nvarchar](max),
    [Day2_CurrentWt] [real] NOT NULL,
    [Day2_FCR] [nvarchar](max),
    [Day3_ChickCount] [int] NOT NULL,
    [Day3_DeathCountPerDay] [int] NOT NULL,
    [Day3_DeathRemarks] [nvarchar](max),
    [Day3_TotalDeathCount] [int] NOT NULL,
    [Day3_FoodConsumption] [real] NOT NULL,
    [Day3_AvgConsumption] [real] NOT NULL,
    [Day3_Remarks] [nvarchar](max),
    [Day3_InStock] [nvarchar](max),
    [Day3_CurrentWt] [real] NOT NULL,
    [Day3_FCR] [nvarchar](max),
    [Day4_ChickCount] [int] NOT NULL,
    [Day4_DeathCountPerDay] [int] NOT NULL,
    [Day4_DeathRemarks] [nvarchar](max),
    [Day4_TotalDeathCount] [int] NOT NULL,
    [Day4_FoodConsumption] [real] NOT NULL,
    [Day4_AvgConsumption] [real] NOT NULL,
    [Day4_Remarks] [nvarchar](max),
    [Day4_InStock] [nvarchar](max),
    [Day4_CurrentWt] [real] NOT NULL,
    [Day4_FCR] [nvarchar](max),
    [Day5_ChickCount] [int] NOT NULL,
    [Day5_DeathCountPerDay] [int] NOT NULL,
    [Day5_DeathRemarks] [nvarchar](max),
    [Day5_TotalDeathCount] [int] NOT NULL,
    [Day5_FoodConsumption] [real] NOT NULL,
    [Day5_AvgConsumption] [real] NOT NULL,
    [Day5_Remarks] [nvarchar](max),
    [Day5_InStock] [nvarchar](max),
    [Day5_CurrentWt] [real] NOT NULL,
    [Day5_FCR] [nvarchar](max),
    [Day6_ChickCount] [int] NOT NULL,
    [Day6_DeathCountPerDay] [int] NOT NULL,
    [Day6_DeathRemarks] [nvarchar](max),
    [Day6_TotalDeathCount] [int] NOT NULL,
    [Day6_FoodConsumption] [real] NOT NULL,
    [Day6_AvgConsumption] [real] NOT NULL,
    [Day6_Remarks] [nvarchar](max),
    [Day6_InStock] [nvarchar](max),
    [Day6_CurrentWt] [real] NOT NULL,
    [Day6_FCR] [nvarchar](max),
    [Day7_ChickCount] [int] NOT NULL,
    [Day7_DeathCountPerDay] [int] NOT NULL,
    [Day7_DeathRemarks] [nvarchar](max),
    [Day7_TotalDeathCount] [int] NOT NULL,
    [Day7_FoodConsumption] [real] NOT NULL,
    [Day7_AvgConsumption] [real] NOT NULL,
    [Day7_Remarks] [nvarchar](max),
    [Day7_InStock] [nvarchar](max),
    [Day7_CurrentWt] [real] NOT NULL,
    [Day7_FCR] [nvarchar](max),
    [SupervisionReport_Id] [int],
    CONSTRAINT [PK_dbo.WeeklyReports] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_SupervisionReport_Id] ON [dbo].[WeeklyReports]([SupervisionReport_Id])
CREATE TABLE [dbo].[SupervisionReports] (
    [Id] [int] NOT NULL IDENTITY,
    [Log_Id] [int],
    [ExtraData_Id] [int],
    CONSTRAINT [PK_dbo.SupervisionReports] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Log_Id] ON [dbo].[SupervisionReports]([Log_Id])
CREATE INDEX [IX_ExtraData_Id] ON [dbo].[SupervisionReports]([ExtraData_Id])
CREATE TABLE [dbo].[ExtraDatas] (
    [Id] [int] NOT NULL IDENTITY,
    [Data1] [nvarchar](max),
    [Data2] [nvarchar](max),
    [Data3] [nvarchar](max),
    [Data4] [nvarchar](max),
    [Data5] [nvarchar](max),
    [Data6] [nvarchar](max),
    [Data7] [nvarchar](max),
    [Data8] [nvarchar](max),
    [Data9] [nvarchar](max),
    [Data10] [nvarchar](max),
    [Data11] [nvarchar](max),
    [Data12] [nvarchar](max),
    [Data13] [nvarchar](max),
    [Data14] [nvarchar](max),
    [Data15] [nvarchar](max),
    [Data16] [nvarchar](max),
    [Data17] [nvarchar](max),
    [Data18] [nvarchar](max),
    [Data19] [nvarchar](max),
    [Data20] [nvarchar](max),
    [Data21] [nvarchar](max),
    CONSTRAINT [PK_dbo.ExtraDatas] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Liftings] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [DCNo] [nvarchar](max),
    [TotalWt] [nvarchar](max),
    [AvgWt] [nvarchar](max),
    [TraderLog_Id] [int],
    CONSTRAINT [PK_dbo.Liftings] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TraderLog_Id] ON [dbo].[Liftings]([TraderLog_Id])
ALTER TABLE [dbo].[Stocks] ADD CONSTRAINT [FK_dbo.Stocks_dbo.Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[VendorLogs] ADD CONSTRAINT [FK_dbo.VendorLogs_dbo.Vendors_Vendor_Id] FOREIGN KEY ([Vendor_Id]) REFERENCES [dbo].[Vendors] ([Id])
ALTER TABLE [dbo].[ItemTransactions] ADD CONSTRAINT [FK_dbo.ItemTransactions_dbo.Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[ItemTransactions] ADD CONSTRAINT [FK_dbo.ItemTransactions_dbo.VendorLogs_VendorLog_Id] FOREIGN KEY ([VendorLog_Id]) REFERENCES [dbo].[VendorLogs] ([Id])
ALTER TABLE [dbo].[ItemTransactions] ADD CONSTRAINT [FK_dbo.ItemTransactions_dbo.FarmerLogs_FarmerLog_Id] FOREIGN KEY ([FarmerLog_Id]) REFERENCES [dbo].[FarmerLogs] ([Id])
ALTER TABLE [dbo].[FarmerLogs] ADD CONSTRAINT [FK_dbo.FarmerLogs_dbo.Farmers_Farmer_Id] FOREIGN KEY ([Farmer_Id]) REFERENCES [dbo].[Farmers] ([Id])
ALTER TABLE [dbo].[TraderLogs] ADD CONSTRAINT [FK_dbo.TraderLogs_dbo.Traders_Trader_Id] FOREIGN KEY ([Trader_Id]) REFERENCES [dbo].[Traders] ([Id])
ALTER TABLE [dbo].[ConsumptionLogs] ADD CONSTRAINT [FK_dbo.ConsumptionLogs_dbo.Items_For_Id] FOREIGN KEY ([For_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[ConsumptionLogs] ADD CONSTRAINT [FK_dbo.ConsumptionLogs_dbo.Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[CostSheets] ADD CONSTRAINT [FK_dbo.CostSheets_dbo.Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[VendorPaymentLogs] ADD CONSTRAINT [FK_dbo.VendorPaymentLogs_dbo.Vendors_Vendor_Id] FOREIGN KEY ([Vendor_Id]) REFERENCES [dbo].[Vendors] ([Id])
ALTER TABLE [dbo].[ProductionLogs] ADD CONSTRAINT [FK_dbo.ProductionLogs_dbo.Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [dbo].[Items] ([Id])
ALTER TABLE [dbo].[WeeklyReports] ADD CONSTRAINT [FK_dbo.WeeklyReports_dbo.SupervisionReports_SupervisionReport_Id] FOREIGN KEY ([SupervisionReport_Id]) REFERENCES [dbo].[SupervisionReports] ([Id])
ALTER TABLE [dbo].[SupervisionReports] ADD CONSTRAINT [FK_dbo.SupervisionReports_dbo.FarmerLogs_Log_Id] FOREIGN KEY ([Log_Id]) REFERENCES [dbo].[FarmerLogs] ([Id])
ALTER TABLE [dbo].[SupervisionReports] ADD CONSTRAINT [FK_dbo.SupervisionReports_dbo.ExtraDatas_ExtraData_Id] FOREIGN KEY ([ExtraData_Id]) REFERENCES [dbo].[ExtraDatas] ([Id])
ALTER TABLE [dbo].[Liftings] ADD CONSTRAINT [FK_dbo.Liftings_dbo.TraderLogs_TraderLog_Id] FOREIGN KEY ([TraderLog_Id]) REFERENCES [dbo].[TraderLogs] ([Id])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](255) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId])
)
BEGIN TRY
    EXEC sp_MS_marksystemobject 'dbo.__MigrationHistory'
END TRY
BEGIN CATCH
END CATCH
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201501162117152_FirstMigration', 0x1F8B0800000000000400ED5DDB721B39927DDF88FD07069F66276244F16E774833614BD684A2DB6EB5E4EE795494C9925CD145524B161DD6B7CDC37ED2FEC2D4BD006402854B16AB28EB4DC22501649E44014900E7FFFFFD7F67FFF8BE0A7BDFFCED2ED8ACCFFBC393D37ECF5F2F36CB60FD78DEDF470F7F7BD3FFC7DFFFFBBFCE3E2C57DF7B7F14E5C649B9B8E67A77DEFF1A454F3F0D06BBC5577FE5ED4E56C162BBD96D1EA293C56635F0969BC1E8F4F4ED60381CF8B1887E2CABD73BBBDDAFA360E5A7FFC4FF5E6CD60BFF29DA7BE1C7CDD20F77797A9C73974AED7DF256FEEEC95BF8E7FD9BCD3E8CB6CF27975FE25A91FF3DDAF57BEFC2C08B7B72E7870FFDDED3E4A7DF77FE5DB4DDAC1FEF9EBC28F0C2CFCF4F7E9CFFE0853B3FEFF74F4F13DDAE9F8E92AE0FBCF57A13C5E2366BABA1F7CB41C5C3FA100F3F7A4EBA950EEDBC1FF7787BB3DD3C04A1CF168C8BFEEC3F730971525CF0C9DF46CFB7FE0353FD7AD9EF0DF8BA03B1725955A897F4E4BC7FBD8EC6A37EEFD33E0CBD2FA15F2A2CD6E85DB4D9FAFFF4D7FED68BFCE58D1745FE36C6C1F5D24F47025A46DA49FE2A5A8AAD1303ACDFBB0ABEFBCB5FFCF563F4B56CEDA3F7BD4889FFECF77E5F07311EE34AD176EFB3BDCBFE671B3E1B548A55AAFB0F7FBDDC6C6D346DA3E54368985CBB797975ABEF96CBADBFDB1DBEE1C4F7BD45F46973F8A6AF77977EE8C7362A9A7EBFD984BEB7361674B9F7EB706105EE2B6FBBF25FC1FD0A6E2B70BF5B44C137DF15DB964EA28DF1EBC85FBD22DCD5DA497B65ABF1E2E9241EC5E2CF2CD55054DC91E868E6626D98A5FA784938FB6DEFE5E5F43F3CB1944FDEB7E0315DFB8A7D4EFCB077EB8769E6EE6BF0C422E93ECBBEDA6E56B79BB0D0679A7A7FB7D96F1709FA3620EBB3B77DF423A7A5DD2F9BC79764B6CBB856D14AF2F7E76065EE2C37DEF32A6ECED0F4B890649FD6F0621EB67DB509830DF157116D570AF77CDF8001BEC4DD7D51A882BD9807C00F0A602E50E787BB9A7EE565B06EA5598A5E65F94E7E9988F8BCF5D6BB786593ECA15F9077FE663C9F8A4EB50D16467B01AB2959B000989CB17C8009B4901330B2EDCAEB84DDF084ED475F37CB834FD9E96E22D6D755E83DBAEE297E091E98959E9D363E6F222FBCF4BDE82B95BBE5BB6DCCE14A64DF17852A6713F380A38102749F844A34F82408598A5E117C12E27964F9E23CFFE26BB0F8D35F5F6CF6CE9E6BF1557801338814D0195C50449748BA2F0A559016F300A641010250BF2444BF46DF3A1ECE90BA6C5311E65845BBFDEA2971C217367B93ACDB2C832C8294DB6492235BA54876ADBC25EFAFF87D2BCC05932752C466A1A2D33971BF8264D775CF7DB772B1D945775FFDB8FE2BEA015EFD1B7FFBF3A31BECDFDFDCDD3B6FA81321EF568EEB9EF7141DA1E8C715413FAEDCFB71EBAFBCED9FB45F64B315A062A6C8BD129924D81C647EE0B20922CFF9A7F7F5BB882CE956169B22FB586C65096948161491C440613927A4C42359EE17AFCB275C88F964673E617016009306CC05B8408A3841E25FBEFF67185BF869B37D514B8B645C9F4C97B1E2BCF1EDF15FC7F2ED12D7DD17B7076FF3D27B1EDEA761288220542A2C0D98A6C2E2055E9C4225B22DABA43DA842C1546ABADA6C96CC86A31C563CA8D07C1A4C45C6D02796D8AACEAFD7D9898A765C62BFDDC693563595B8A8B125C71E513AF688DEB147AD3BF688DCB147F48E3D2277EC519B8E3D6AD3B147948E3D6ACBB1C7948E3DA677EC71EB8E3D2677EC31BD638FC91D7BDCA6638FDB74EC31A5638FDB72EC09A5634FE81D7BD2BA634FC81D7B42EFD81372C79EB4E9D893361D7B42E9D893B61C7B4AE9D8537AC79EB6EED85372C79ED23BF694DCB1A76D3AF6B44DC79E523AF6B42DC79E513AF68CDEB167AD3BF68CDCB167F48E3D2377EC599B8E3D6BD3B167948E3D6BCBB1E7948E3DA777EC79EB8E3D2777EC39BD63CFC91D7BDEA663CFDB74EC39A563CF0FE0D8FA5737F771EFBE05C97B252FE05755E92FDCC90102F4FEA538FCFBB4247315132B006F65A2A54C4FF86555F1CB08B085B2B4AAB379218D0E17254D3BFDE17BB4F52EBDC8D3EC36535ED5F1B29846D7ABB24E670BAA9E1DB30BC0A326DEB0852933F246ADB43A6EA5D5492BAD4E5B6975D64AABF3565A7DD34AAB6F5B697578DA4EB3EDCC4EC376A6A7613BF3D3B09D096AD8CE0C356C678A1AB633470DDB99A486EDCC52A37666A951D3B394F62A35B9DE9D74E065AD517DE7E3D097178778F24468340D0331318183BD17C01EF56DED8EB16C539D43F49E29556DF54026D8E0C11266DBBAFD8A8D6F544F755DEF92E715AA074E7F5FC7E2C3E7B82916E0BC063EFAAB2FFE369795547808D6C9BDD33FBC701FA79C0295711592985E76683E2F3F5497CF2E4D703546EA1AF935FAB2F8182A2953079BF86EB7DB2C82D46AAC96EE91D7E23EAC973DF99B5419FCCA77AC62B8EDC328780A83456C98F3FE5F41DF517185751971990A7869A72727437174CC48D40304CF0EC9FA257F83A8EA5DF57696C180A56F1701C1A403475FB391F551FDB40D6F1EF691220335281FC6390C02B2D73CEAED243CEDA1617FD841A568C9B0F5356B307CF0C88AAC93F21757AA5E566F1119585EFA520B104C6A77F11597FAEE49ED2E1D768DDD65AFC11CC2EEE0C90F5927E5EF7F54BD643EE8FA7697BE1B020493DA1DB9B22FEBA1EAFE7ED547E13507030D282EFF3739E361EF0268765136F193E9E060D3BE70E559DE3DFCFE333BF4E24503A351A3F7A69BFFCEC14BBDEA8F92E286AFF8DD636E6E1B2F7FE417849B5D0521F758651D555D6AADFAC85F4D36D083E2426C93A8C07FE7942ECED53F7A32CB7EF0EBB1C91640F9A3A9DD77D74925C52FAAFA1D063FAF1AA8A666D150FF0B2DD318772D9A6CEDA0FAF156BFDBC82FB98D0108FE12CCB4C5FCF24C082318EC90755711F9A83A5984F70CD4208F97E82CDD0C069F055CD287BF827519824834FADEDBF939CF0C187E52E7CE8F8A184AC5E0B2EBF7AA104EDE4D96E0058E9817557C27809022A3A67EB1D900F58B8C9AFAD9240D6A67C93575F37809A89CA76B8D3C35A664F0699E46FF993D07660EB02DD15229DE3126AF460A83552085C9D392828DAAD86DD408E097AC982071395C2B305F0DE2B2CAF5A596F5F3A514260AAED36A24728B124CA2B0DCA911C77E8D3069FCD7AA46985C0CF2EDA891C57C01803426AF464A39450319658E2081994F8529207F538529C04E04C84AB52E265B76B69C63065AF58B4F06531F9DC806FC5834C6095F7C87A3558767F502B44CCFA573A05E44164822D002FEAA38D4447DBC563F622BD8929FEB158A51C6689B0448FECEB20A1F48104F2B7CEB800E3E6867A0540B5DC0B7B0A132D4A15CBD602E330AE977592F7A0B24916A418A086558572BB0EBA082432202BED90C95A10EF2EA8579995148D7587A715D2089420DD80BAC501175415FDDB02F3304B00A54A84411E8A59F36D1675F6B5552F33D51C5816994D2F0C7447CE21253883C22AC1313E6D4502DE8951A40A3C04D7D4991071D655FD49A30B151A0187C61AB1D4AED67561E176E622D86BD6A0835541733D68D1A332310F7590ABD28E2C4F4B891DC82417625F5616483403233907297A7DAAD28E3C53A9F7312D5945DD5510F1A52360C2A3BAB490C1C330285AD7A03EA62F6D53A0A9344988D63CCCE4A83A16446A43C5860AE32E4B41C54544D945933CECC8C4012AAD00C2CEB2C1615AA28CEEE95A1E432EF6C905199E709670309E7F9D947EFE929EE1BC3819EA7F4EE3202F48BBFDD991390AF32198305E7B162E0BB6C29DA6CBD475FC84DBEF74BFF2AD8EEA204215FBCE4ACE1C572058A6906CE8BD690F839345D11902A2A257FE71F3D40087F22655367F579150F31F936A7A3F519C32B6BF7124E7A2FF4B6C2C1D18249FD6213EE576B3E4D44A25A4AF2179493A5424967036138A2EA0640770298457B68594BBAA832B2132E45C344B28A32BD8A9631B30AB488CC1A320925990D2BA44CD497C370D3B09298647D590CD90CA79A2A595F564A34C34AB98487AE33B4B582566938C908ADB8140DB4CA2ABEA2D504AD0531390FD622B559DCB7845BC9DECC08B5980C0DCCE2D5BA8AD8EC1E042B214B31F8F2A6FCE5DC57374D7991B84268CDCD81850AD14096A45E33D0AAA8B85819556A674C22A12DB75D532182B4975568DD66CC935D90E3160E698ABE8492E1921552261ACBC978CD11595986BEBC92AB9C955526760676E0771D82AF8D94E53B15A8F3E1514A6868A20073043E3D48F19391B572C8C9923A636B09D1B6ED42D86E8A51D4FD51A6989C47179B64F22C83A53AC7AFCDADD7B91C7D8905CB362BAB48335890313CDBDCB28C49EF8C63A8628F468E2115A4E1188ABACD38064F54CD6DD0B89C66E6C02E3B5BAB3074FE04E32725B411783C9BBE6E8629E8826A2C893382DF6E85D8C483138E2056314CA7F2EAC05C27A0AB8B0DF30DAB4C52CE1FCD8AC9933A049AF29889335E505A664DA848EB7615250CD7B260DF22595F5649B8CC4A2A13CDE4A41CC3A29C34D1400ED61DF3DE609D31EFCB15D2972BE3BE5C217DB932ED4BF9443167F122B1333E2D1C91228966C9389553897A412D9588AE7A79C197CCAD6F56B255794BF6168E7E39DAFB46CE8CAC65EC9AFA5DB5B46EF4A9251BF387A91C4D2C673AD6B2B0BA7A33062E488C5909459A813B676F9B71DE9C2535F7119086A79387DAB9D0749260027981639847BF90692817122B00E9B0884D1BA836916C43D98043018807250C5B009C0AA00550C2B00591620134201630942F57BDADD64B160520B1CC31C577458D00E15DE599DA4E74BD2AD548D248E57F237BFFC35881817447FF13698271F97648400880817837FF430881410B6EFE07F98141034EFE3792FADFC8D2FF4652FF1B59FADF48E17F236BFF1BA1FE37B2F0BFB1CAFFC6F6FE8791F702E98EFE27B2F9E2F2ED9080F0F402F16EFE87F0F68216DCFC0FD2F882069CFC6F2CF5BFB1A5FF8DA5FE37B6F4BFB1C2FFC6D6FE3746FD6F6CE17F1395FF4DECFD0FE3D805D21DFD4F24DDC5E5DB2101A1D305E2DDFC0FA1D7052DB8F91F64DB050D38F9DF44EA7F134BFF9B48FD6F62E97F1385FF4DACFD6F82FADFC4C2FFA62AFF9BDAFB1F46850BA43BFA9FC88D8BCBB74302C27A0BC4BBF91FC2820B5A70F33F488A0B1A70F2BFA9D4FFA696FE3795FADFD4D2FFA60AFF9B5AFBDF14F5BFA985FFCD54FE37B3F73F8CB1164877F43F91C216976F8704849C168877F33F84AC16B4E0E67F90BB1634E0E47F33A9FFCD2CFD6F26F5BF99A5FFCD14FE37B3F6BF19EA7F330BFF9BABFC6F6EEF7F18B12C90EEE87F22D32C2EDF0E0908872C10EFE67F08A72C68C1CDFF20C52C68C0C9FFE652FF9B5BFADF5CEA7F734BFF9B2BFC6F6EED7F73D4FFE632FF6BE9072FA29FBA6A38685391B5D765EA65B8FDE8D5928A55EF211829592A4843B98ABA8DFD549CB0960ABF157BE0BDE43A1923280310EAD4C9184319635319132863622A630A654C4D65CCA08C99A98C3994313795F106CA78632AE32D94F1D61863A708C800A953AD140CAAC6581D22601D1AA37588C075688CD72102D8A1316287086487C6981D22A01D1AA37688C076688CDB2102DCA1317287087487C6D81D21D81D1963778460778462B7A50F20CA7768FEF99388D1F8F8496B76F59454C684C849B8303B8253D21AB242CAC4668EF2340D2FFE3122E4A277FE105CFD65EEBCA0D685EDE435254129F0D165A80B3D5C2522307025AA285B35EA50FE50946D874CFB12EB7519A45C0AD7BB848AB2A466D418A6F8BC94B1D1E1BBD47A17C6CBE2DAD7C2119DCBDE9EB6D47C2E84000CB2E7B53B0509E9709D41813FD36D72975B323BD4DCD7462CA17A85BBE51943F5EA78A780D2FCDC913F50AD3975E4A5DDC12179E6DC69FA48A551CE20DC33D99DC2856AC0CEE0806FB8EB3D135016D77E0C00D1BEEC9D764BF5E7420860217B8ABE53B8900E9710143A330628ED3E63489EC177C205D18C217958BF83C86868C6806FFCEBBD9F5016D77E2501D1BEEC1D7F4BF5E742086021A32AE8142EA4C3750605C6786070193CAB6076DF1B31829CD2C076E6A0D9A7C8C91B3A050F7CB4D4D8D0D8A5A035A8D1D1A12D8A82C8A25300696A8722F260E83D0B20C585E4EA3FAA788CECA2753460541E3F0410A49C204617CA6B825EF26BE3D20DA294F2A323213029D149A750D35C24EC062149D1BF932E99486E54F7CE115340991D9950908E75111E4D4D2A12C218B3533E591DE3833CD82F182A66184B8310ED6D950C389DC24A437B5B05818E215CCA7A266F1D68994460DCB1340B94DB087E04369F4E614847050D208AA1E331C41453B3998908D00859DAAB92D308A8008152A760A51ABC339C10FE259D93246C79CD23238825A4144B4ED137A26F979448AA53E0500DB91E1C808A4A2C529E4BC953CAFF4B2AAA9C068AE3A74A5592B04DA5AAD8E59454222F5456A4DF8BC7FF2D58269C5077CFBB78C976921438B9FBDFF0220CD2075D8B021FBD75F0E0EFA2CF9B3FFDF5797F747AFAA6DF7B1706DE2EE3093367BCF297ABC16EB70C11BEABC40B724B4AB99ECE7EF681D50A6BDEFA0F4C75CC3862E533014045BD8C623D483491BADA3FFDD8505EE42F6FBC28F2B7EB045A7EFEF2638213EF4B425F96636550DB44F257D1C8FA9BB75D7CF5B67F5979DFFF8795166DC58744D913494AED61344C7A8AB3515AC30AD3529696A4F20D5A0A61CC33B414E298976833715F1296134321E9D3B38C2994D5B5D184D124BDA2A9E3682AF897ECC16481486D484116A35740E5D448BAEE8B7C595262A49626236DD323A7568FD6F6D5F3CFF6562B2366A808E74500087B1EADB2B353E95903CBF8EF28481CCF5048C958606F308E78C8780D07E595E44304B298E83C359C9407808E1654BFB9396F4EA37138EF9799BCD88D5A8BE18F1B51834772A0F06861D3B5B928A74C219841787A22FBD563414D643F369692C85E0A73C0921AD492336F470B6A9E5EA8C549B1938EC19CBB6B06482F04453FCA069B205CC3120691476D5447EB8E165C24DF5D8A5D5BCED8E3F05D325BA61F70DF28397BF763638661F0B1B77949DEE32622E5A77110E1DE09F73E5C39F7E1CAB50FE57B4E048B83868338B2D38E3FB64F16943BF61068305CA23878F8635BCDD1EF1BF334F999AEA3B557418563AFEDFCF18C6E4CB2E9537404722E451A1CFBE15DE29C3714022935778972DB38F612BCE498C9DBFA5E6827507CB9D1551EB906CBF71AC940583DD6E83A5842E718D139074648432190D8B408F18C632F899D03D2CAB8CA23D720B1738CE89C6344E91C633AE7C0D8622804129B16618571EC25B17340CE175779E41A24768E319D738C299D6342E71C18950B854062D322942D8EBD24760E48C8E22A8F5C83C4CE31A1738E09A5734CE99C03E359A110486C5A844FC5B197C4CE01D9525CE5916B90D839A674CE31A5748E199D736024281402894D8B909D38F692D839209589AB3C720D123BC78CCE396694CE31A7730E8CA1844220B169112612C75E123B07E419719547AE4162E798D339C79CCC39F0EBBFD43F0DD45CCC3DDADF079C0F8DF2D764A9D52E2101395A75E7842224EE98D28A10491A93499A90499A92499A91499A93497A4326E92D99A48485844A141DCA8774301FD2E17C4807F4211DD28774501FD2617D4807F6211DDA4774681FD9A05DFB2388BEEC70CC9F40F7132419B90881F54A82110259DC49098283E644377198D7288AC56CC528C277E3C37AD9BBDD24E2ABB7D7B2471E4EB2848FFB300A9EC26011B773DE3F3D3901AC3D8C0C6EEF910AC95378297F052262FCFAC93623F0C2647F152F010348CD72B30DD68BE0C90BD91E0B85347D22D15D294ECCB9F49FFC7502766E545A0DC9DF7D2B850ABE593772EE6D11B591017788CC4CECDB8CB99D8A24237357974DA1A834B511B363836BC6F052AA15B431E5D39007303FCA10D2A4B78B974345715CDE51CF0035EFE0777A2EC89EF62772E1CE23C2CC655B80451D09CA01B021D23EC8AC59DDDF65EDC8A41E1936A404275DC1461DDDC541B1813E002482034186212C0C41460A8543E0C00C744A229C03981F707EC8CCC63288E4362B928CCC2F3C87C8896ACEFCD8E09A31BF9422056D4CC96A7200F323DC1E4D2E2085DBAAAC3431EBA8978F6AC20B7C2268732781B178BCE2A0151CB4BB8B10183B9AC540710199377F997AE49697509B74D0E832528EC34493985BAF50229BF922624B7246934E86986E20F946937302D71C274CC839EAB9E146C556D2C1F9414E9920B3233C4AC3FD4400738D50C2DDE16505F3198D60A4F6F5FE2600A322A240DBD3A58C68053CF0A67C53112847181E6314CA0E9FAD46A054EC1932CB0ACC1CB94599D423C48964D45DC2491D5BC801D002693288224D3518294E68B0B2CAB4066356079A3724D4228AA0D501E78C0F3C9546B95715682F001272E294BC3C4B36D1ABCE51E4B6E47233028DF3FEF24B7204253B8DC1150030E19B2AB643A09592EE106920CBDBD50A2FA2EE4078C9F38E08CFF2EA85678B76203AA7DD4304E744E23562F35321406E9E8E094EB3EA2533BF5C4AB49D51E948151E67EBA985FBBD0AD5105742A62CA690AEADF1D13179728B6B8D8E9913412B2C1D11D24A99ADDB8AB40995FC7AE162D810342216C01AE3CBE8345A06AB90F6CA3CBCA93C5B17E16C24440274B6881CEF55A9FAA6855D376857C8C71AE58AD4B7C86FE140837C36D61E5B42635E826B3B3847C132E87C2516AB6F9D59AF8256590639A4B532BBBE95729502DA2873B016F24C289FA7BE82E70D7B4C01769A47C2474800A9EC7D9100D617E0F42153254F11CFC3F25DD6180E3859870C4A7DFA0E8D98323D2D9214C3039F36A6629AEA3C4CF4041932D4FA93668E96947C5FCBCA5C1EA175F3D58BCAB8C89119172BB5386CF11810326CE5492159A486E93493DAC161170B64D5B8F18322C83918306AE5900DD565314C70920319A6FAB407B27BE67A5B2429860956924C459A612227169081D69D6B709CAEF0D5665A57CCA21EB06C8AAEFD09FFA886CCFD0A8D8E56FE3BB5F34085457D3EC63295E8F3037F6F957E866A7E9A255B6AC03D06539FCD745601F2BB2232FABA5F1F1D2D8DEE6ED2AA428EF37015A4E3C86259EFE7B6BA0836BB2A86B90AB5605B30C887DE84525265EB2804098A122D4D1C1549A20666EBA7A30CE9EF06F8EF25CC7098D44EA904A1C2868AA8F921C065598207FE999A659AF650011F74997736C876DF7942FC2FE07D3E1BDCEED7C925CAECBF4B7F173C5622123AEBB5BFE082E66599EBF5C3A688E00B3D2A8A0817133FFA91B78C31F16E1B050FF1223CCE5EF8BB5D3ADC3FBC709FA069F5C55F5EAF7FDD474FFB281EB2BFFA123EB3CA487E0350B57F36007D3EFB355D40EC288610773348EE9DFEBA7EBF0FC265D9EF2B8468452222F97121BF119BD8324A6EC63E3E97923E6DD69A8272F595BF897CF6574F612C6CF7EBFACE4BA85B657DABD721AFB1B3CBC07BDC7AAB5D2EA3AA1FFF1BC36FB9FAFEF7FF00DE46620473640100, '5.0.0.net45')