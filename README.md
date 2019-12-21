# Titan
# Todo
- [R1] [Done] Remove TestGroupId, use Nunit Category attribute Instead
- [R2] [Done] Remove TestSuiteId on whole project
- [A1] [Done] Add Nunit attribute: isNotCompatible for the sqlite database, not for testcase, this field is update manually from DashBoard
- [A2] [Done] On teardown, get array of drivers, then close its
- [A3] [Done] Add attribute isDebug for TestCase, if true, do not close drivers
- [A4 - Relate to A3] Check if testcase isDebug Mode, the other testcases run after this will not init new Webdriver, and Notify user
to close manually Browser and webdriver.exe in TaskManager
- [A5 - Relate to A4] In Setup of testcase, should display all information of testcase including warning message of [A4 - Relate to A3]
- Incase of user add new comment, update to DB with concat string not replace