document.addEventListener('DOMContentLoaded', function () {
    const path = window.location.pathname;
    switch (path) {
        case "/User/AddUser":
            goToAddUser();
            break;
        case "/User/GetAllUsers":
            goToAllUsers();
            break;
        case "/Pos/AddPos":
            goToAddPos();
            break;
        case "/Pos/GetAllPos":
            goToAllPos();
            break;
        case "/Pos/GetPosById":
            if (posId) {
                goToPosDetails(posId);
            } else {
                goToHomePage();
            }
        case "/Pos/UpdatePos":
            if (posId) {
                goToPosEdit(posId);
            } else {
                goToHomePage();
            }
            break;
        case "/Issue/GetAllIssues":
            goToAllIssues();
            break;
        case "/Issue/AddIssue":
            goToAddIssue();
            break;
        case "/Issue/OpenIssue":
            if (posId) {
                goToOpenIssue(posId);
            } else {
                goToHomePage();
            }
            break;
        case "/Home/Dashboard":
            goToHomePage();
            break;
        //case "/Home/Index":
        //    goToHomePage();
        //    break;
        default:
            goToHomePage();
    }
});

window.addEventListener('popstate', function (event) {
    if (event.state) {
        switch (event.state.page) {
            case "AddUser":
                goToAddUser();
                break;
            case "AllUsers":
                goToAllUsers();
                break;
            case "AddPos":
                goToAddPos();
                break;
            case "GetAllPos":
                goToAllPos();
                break;
            case "PosDetails":
                if (event.state.posId) {
                    goToPosDetails(event.state.posId);
                } else {
                    goToHomePage();
                }
                break;
            case "UpdatePos":
                if (event.state.posId) {
                    goToPosEdit(event.state.posId);
                } else {
                    goToHomePage();
                }
                break;
            case "GetAllIssues":
                goToAllIssues();
                break;
            case "AddIssue":
                goToAddIssue();
                break;
            case "OpenIssue":
                if (event.state.posId) {
                    goToOpenIssue(event.state.posId);
                } else {
                    goToHomePage();
                }
                break;
            case "Dashboard":
                goToHomePage();
                break;
            //case "GetAllPos":
            //    goToAllPos();
            //    break;
            default:
                goToHomePage();
        }
    } else {
        goToHomePage();
    }
});