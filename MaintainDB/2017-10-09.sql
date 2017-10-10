--合約、收款、上課
SELECT  RegisterLesson.RegisterID, RegisterLesson.RegisterDate, RegisterLesson.Lessons, LessonPriceType.Description, LessonPriceType.Status, 
               LessonTime.ClassTime, LessonTime.LessonID, IntuitionCharge.Payment, TuitionInstallment.InstallmentID, TuitionInstallment.PayoffAmount, 
               TuitionInstallment.PayoffDate, Payment.PaymentID, Payment.PayoffAmount AS Expr1, Payment.PayoffDate AS Expr2, 
               LessonAttendance.CompleteDate
FROM     LessonAttendance RIGHT OUTER JOIN
               UserProfile INNER JOIN
               RegisterLesson ON UserProfile.UID = RegisterLesson.UID INNER JOIN
               LessonTime ON RegisterLesson.RegisterID = LessonTime.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID ON LessonAttendance.LessonID = LessonTime.LessonID FULL OUTER JOIN
               TuitionInstallment INNER JOIN
               Payment ON TuitionInstallment.InstallmentID = Payment.PaymentID FULL OUTER JOIN
               IntuitionCharge ON TuitionInstallment.RegisterID = IntuitionCharge.RegisterID ON RegisterLesson.RegisterID = IntuitionCharge.RegisterID
WHERE   (UserProfile.RealName LIKE '%余星潔%')