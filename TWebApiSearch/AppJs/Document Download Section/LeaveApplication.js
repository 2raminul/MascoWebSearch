$("#btnLeaveApp").on("click", function () {
    //window.location.href = "/LeaveForm/LeaveForm";
    window.open('/LeaveForm/LeaveForm','_blank');
    //window.open('LeaveReport?ID=10');
});
$("#btnHourlyLeave").on("click", function () {
    window.open('/ShortLeaveForm/ShortLeaveForm','_blank');
    //window.open('HourlyLeave?ID=10');
});
$("#btnOutStationDuty").on("click", function () {
    window.open('/OutstationDutyForm/OutstationDutyForm', '_blank');
    //window.open('OutStationDuty?ID=10');
});
$("#btnIncomeTaxCertificate").on("click", function () {
    window.location.href = "/IncomeTaxCertificate/IncomeTaxCertificate";
    //window.open('LeaveReport?ID=10');
});
$("#btnSalaryPayslip").on("click", function () {
    window.location.href = "/SalaryPayslip/SalaryPayslip";
    //window.open('HourlyLeave?ID=10');
});
$("#btnAdvancePayslip").on("click", function () {
    window.location.href = "/AdvancePayslip/AdvancePayslip";
    //window.open('OutStationDuty?ID=10');
});
$("#btnBonusPayslip").on("click", function () {
    window.location.href = "/BonusPayslip/BonusPayslip";
});