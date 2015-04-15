  Update [gcr_db].[dbo].[Media]
  set filename = SUBSTRING([title],31,200)
    where title like 'C:\Users\admin\D%'