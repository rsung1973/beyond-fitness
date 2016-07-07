<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../scripts/jquery-3.0.0.js"></script>
    <script type="text/javascript" src="../scripts/moment.js"></script>
    <script type="text/javascript" src="../scripts/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-datetimepicker.js"></script>
    <!-- include your less or built css files  -->
    <!-- 
  bootstrap-datetimepicker-build.less will pull in "../bootstrap/variables.less" and "bootstrap-datetimepicker.less";
  or
  -->
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.css" />
    <%--    <script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery-2.1.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/moment.js") %>"></script>
    <script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/asset/js/bootstrap.min.js") %>"></script>
    <script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/bootstrap-datetimepicker.js") %>"></script>
    <link rel="stylesheet" href="~/asset/css/bootstrap.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="~/css/bootstrap-datetimepicker.min.css">--%>
</head>
<body>
    <form id="form1" runat="server">
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <title></title>
            <style>
                * {
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    box-sizing: border-box;
                }

                body {
                    margin: 0;
                }

                html {
                    font-family: sans-serif;
                    -webkit-text-size-adjust: 100%;
                    -ms-text-size-adjust: 100%;
                }

                input {
                    line-height: normal;
                }

                ::before, ::after {
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    box-sizing: border-box;
                }

                .row {
                    margin-right: -15px;
                    margin-left: -15px;
                }
                /* @media all and (min-width:992px) */
                .container {
                    width: 970px;
                }
                /* @media all and (min-width:1200px) */
                .container {
                    width: 1170px;
                }

                    .clearfix::before, .clearfix::after, .dl-horizontal dd::before, .dl-horizontal dd::after, .container::before, .container::after, .container-fluid::before, .container-fluid::after, .row::before, .row::after, .form-horizontal .form-group::before, .form-horizontal .form-group::after, .btn-toolbar::before, .btn-toolbar::after, .btn-group-vertical > .btn-group::before, .btn-group-vertical > .btn-group::after, .nav::before, .nav::after, .navbar::before, .navbar::after, .navbar-header::before, .navbar-header::after, .navbar-collapse::before, .navbar-collapse::after, .pager::before, .pager::after, .panel-body::before, .panel-body::after, .modal-footer::before, .modal-footer::after {
                        display: table;
                        content: " ";
                    }

                    .clearfix::after, .dl-horizontal dd::after, .container::after, .container-fluid::after, .row::after, .form-horizontal .form-group::after, .btn-toolbar::after, .btn-group-vertical > .btn-group::after, .nav::after, .navbar::after, .navbar-header::after, .navbar-collapse::after, .pager::after, .panel-body::after, .modal-footer::after {
                        clear: both;
                    }

                .container {
                    padding-right: 15px;
                    padding-left: 15px;
                    margin-right: auto;
                    margin-left: auto;
                }
                /* @media all and (min-width:768px) */
                .container {
                    width: 750px;
                }

                .col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 {
                    position: relative;
                    min-height: 1px;
                    padding-right: 15px;
                    padding-left: 15px;
                }
                /* @media all and (min-width:992px) */
                .col-md-1, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-10, .col-md-11, .col-md-12 {
                    float: left;
                }
                /* @media all and (min-width:992px) */
                .col-md-9 {
                    width: 75%;
                }

                body {
                    font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
                    font-size: 14px;
                    line-height: 1.4285;
                    color: #333;
                    background-color: #fff;
                }

                body {
                    padding-top: 70px;
                }

                html {
                    font-size: 10px;
                    -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
                }
                /* @media all and (min-width:768px) */
                .col-sm-1, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-10, .col-sm-11, .col-sm-12 {
                    float: left;
                }
                /* @media all and (min-width:768px) */
                .col-sm-6 {
                    width: 50%;
                }

                .form-group {
                    margin-bottom: 15px;
                }

                .input-group {
                    position: relative;
                    display: table;
                    border-collapse: separate;
                }

                button, input, optgroup, select, textarea {
                    margin: 0;
                    font: inherit;
                    color: inherit;
                }

                input, button, select, textarea {
                    font-family: inherit;
                    font-size: inherit;
                    line-height: inherit;
                }

                .form-control {
                    display: block;
                    width: 100%;
                    height: 34px;
                    padding: 6px 12px;
                    font-size: 14px;
                    line-height: 1.4285;
                    color: #555;
                    background-color: #fff;
                    background-image: none;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
                    box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075);
                    -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
                    -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
                }

                .input-group .form-control {
                    position: relative;
                    z-index: 2;
                    float: left;
                    width: 100%;
                    margin-bottom: 0px;
                }

                .input-group-addon, .input-group-btn, .input-group .form-control {
                    display: table-cell;
                }

                .input-group :first-child.form-control, :first-child.input-group-addon, :first-child.input-group-btn > .btn, :first-child.input-group-btn > .btn-group > .btn, :first-child.input-group-btn > .dropdown-toggle, :last-child.input-group-btn > :not(:last-child):not(.dropdown-toggle).btn, :last-child.input-group-btn > :not(:last-child).btn-group > .btn {
                    border-top-right-radius: 0px;
                    border-bottom-right-radius: 0px;
                }

                .input-group-addon, .input-group-btn {
                    width: 1%;
                    white-space: nowrap;
                    vertical-align: middle;
                }

                .input-group-addon {
                    padding: 6px 12px;
                    font-size: 14px;
                    font-weight: 400;
                    line-height: 1;
                    color: #555;
                    text-align: center;
                    background-color: #eee;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                }

                .input-group :last-child.form-control, :last-child.input-group-addon, :last-child.input-group-btn > .btn, :last-child.input-group-btn > .btn-group > .btn, :last-child.input-group-btn > .dropdown-toggle, :first-child.input-group-btn > :not(:first-child).btn, :first-child.input-group-btn > :not(:first-child).btn-group > .btn {
                    border-top-left-radius: 0px;
                    border-bottom-left-radius: 0px;
                }

                :last-child.input-group-addon {
                    border-left: 0;
                }

                .date.input-group .input-group-addon {
                    cursor: pointer;
                }

                .glyphicon {
                    position: relative;
                    top: 1px;
                    display: inline-block;
                    font-family: "Glyphicons Halflings";
                    font-style: normal;
                    font-weight: 400;
                    line-height: 1;
                    -webkit-font-smoothing: antialiased;
                    -moz-osx-font-smoothing: grayscale;
                }

                .glyphicon-calendar::before {
                    content: "\e109";
                }
            </style>
        </head>
        <body>
            <div class="container">
                <div class="row">
                    <div class="col-md-9" role="main">
                        <div class="container">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group date" id="datetimepicker1">
                                            <input class="form-control" type="text">
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <script type="text/javascript">
                                    $(function () {
                                        $('#datetimepicker1').datetimepicker();
                                    });
        </script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </body>
        </html>

    </form>
</body>
</html>
