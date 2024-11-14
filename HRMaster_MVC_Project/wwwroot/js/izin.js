
    function calculateLeaveDays() {
        var startDate = new Date(document.getElementById('LeaveStartingDate').value);
        var endDate = new Date(document.getElementById('LeaveEndDate').value);
        var leaveDaysField = document.getElementById('LeaveDays');
        var warningMessage = document.getElementById('dateWarning');

        if (startDate && endDate && startDate <= endDate) {
            var timeDiff = endDate.getTime() - startDate.getTime();
            var daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
            leaveDaysField.value = daysDiff;
            warningMessage.style.display = 'none';
        } else if (startDate > endDate) {
            warningMessage.style.display = 'block';
            leaveDaysField.value = '';
        } else {
            leaveDaysField.value = '';
            warningMessage.style.display = 'none';
        }
    }

    function validateAndSubmitForm() {
        var startDate = new Date(document.getElementById('LeaveStartingDate').value);
        var endDate = new Date(document.getElementById('LeaveEndDate').value);
        var warningMessage = document.getElementById('dateWarning');

        if (startDate > endDate) {
            warningMessage.style.display = 'block';
        } else {
            warningMessage.style.display = 'none';
            document.getElementById('leaveRequestForm').submit();
        }
    }
    document.addEventListener('DOMContentLoaded', function () {
        const today = new Date().toISOString().split('T')[0];
        document.getElementById('LeaveStartingDate').value = today;
        document.getElementById('LeaveEndDate').value = today;
    });
