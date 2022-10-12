alter procedure [dbo].[SupportBudgProc1]      
  (@specLst nvarchar(MAX))      
 as      
begin  
	declare @spec nvarchar(MAX)
	while(len(@specLst) > 0)
	begin
		set @spec = substring(@specLst,0,charindex(',', @specLst))
		if len(@spec) > 0
			set @specLst = substring(@specLst,len(@spec)+2,len(@specLst) - len(substring(@specLst,0,charindex(',', @specLst))))
		else
			select @specLst

		if len(@spec) > 0
			select @spec, @specLst
		else
			set @specLst = ''
	end

end;
