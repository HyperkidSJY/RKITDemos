-- dept name , dept subject , fac name , count of depart


select d01f02, c01f02, d01f03 , count(d01f01) from ymd01 join ymc01 on d01f06 = c01f01 group by d01f06;

