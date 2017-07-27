select * from UserComments

select top 1 * from(
select top 100 percent count(blid)num, blid from usercomments group by blid order by num desc
)a
select * from uservotes