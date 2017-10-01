SELECT  s.RegisterID, s.ContractID, s.TypeID, s.UID, LessonTime.LessonID, LessonTime.GroupID, r2.RegisterGroupID, r1.RegisterGroupID AS Expr1
FROM     (SELECT  MAX(RegisterLessonEnterprise.RegisterID) AS RegisterID, RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, 
                               RegisterLesson.UID
               FROM      RegisterLessonEnterprise INNER JOIN
                               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
               GROUP BY RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, RegisterLesson.UID) AS s INNER JOIN
               RegisterLesson AS r1 ON s.RegisterID = r1.RegisterID INNER JOIN
               RegisterLesson AS r2 ON r1.UID = r2.UID AND r1.ClassLevel = r2.ClassLevel INNER JOIN
               LessonTime ON r2.RegisterID = LessonTime.RegisterID
WHERE   (s.TypeID = 4)
ORDER BY s.UID
go
UPDATE LessonTime
SET        GroupID = r1.RegisterGroupID, RegisterID = r1.RegisterID
FROM     (SELECT  MAX(RegisterLessonEnterprise.RegisterID) AS RegisterID, RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, 
                               RegisterLesson.UID
               FROM      RegisterLessonEnterprise INNER JOIN
                               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
               GROUP BY RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, RegisterLesson.UID) AS s INNER JOIN
               RegisterLesson AS r1 ON s.RegisterID = r1.RegisterID INNER JOIN
               RegisterLesson AS r2 ON r1.UID = r2.UID AND r1.ClassLevel = r2.ClassLevel INNER JOIN
               LessonTime ON r2.RegisterID = LessonTime.RegisterID
WHERE   (s.TypeID = 4)
go

DELETE FROM GroupingLesson
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               GroupingLesson ON RegisterLesson.RegisterGroupID = GroupingLesson.GroupID
WHERE   (RegisterLesson.RegisterGroupID NOT IN
                   (SELECT  GroupID
                   FROM     LessonTime)) AND (RegisterLessonEnterprise.TypeID = 4)
go

go

UPDATE RegisterLesson
SET        Lessons = 6, Attended = 100
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
WHERE   (RegisterLesson.RegisterGroupID IS NOT NULL) AND (RegisterLessonEnterprise.TypeID = 4)
go
SELECT  RegisterLesson.RegisterID, RegisterLesson.RegisterDate, RegisterLesson.Lessons, RegisterLesson.UID, RegisterLesson.ClassLevel, 
               RegisterLesson.Attended, RegisterLesson.RegisterGroupID, RegisterLesson.GroupingMemberCount, RegisterLesson.AdvisorID, 
               RegisterLesson.AttendedLessons, RegisterLesson.BranchID
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               UserProfile ON RegisterLesson.UID = UserProfile.UID
WHERE   (UserProfile.RealName = '¦ó¨Ø¬À')
go
SELECT  EnterpriseCourseMember.ContractID, EnterpriseCourseMember.UID, s.UID AS HasUID
FROM     (SELECT  EnterpriseCourseContent.ContractID, RegisterLesson.UID
               FROM      EnterpriseCourseContent INNER JOIN
                               RegisterLessonEnterprise ON EnterpriseCourseContent.ContractID = RegisterLessonEnterprise.ContractID AND 
                               EnterpriseCourseContent.TypeID = RegisterLessonEnterprise.TypeID INNER JOIN
                               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
               WHERE   (EnterpriseCourseContent.TypeID = 4)) AS s RIGHT OUTER JOIN
               EnterpriseCourseMember ON s.ContractID = EnterpriseCourseMember.ContractID AND s.UID = EnterpriseCourseMember.UID
go
SELECT  RegisterLessonEnterprise.RegisterID, RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, RegisterLesson.RegisterDate, 
               RegisterLesson.UID, EnterpriseCourseContent.Lessons, EnterpriseCourseContent.DurationInMinutes, EnterpriseCourseContract.Subject, 
               RegisterLesson.RegisterGroupID
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               EnterpriseCourseContent ON RegisterLessonEnterprise.ContractID = EnterpriseCourseContent.ContractID AND 
               RegisterLessonEnterprise.TypeID = EnterpriseCourseContent.TypeID INNER JOIN
               EnterpriseCourseContract ON EnterpriseCourseContent.ContractID = EnterpriseCourseContract.ContractID
WHERE   (RegisterLesson.RegisterGroupID IS NULL)