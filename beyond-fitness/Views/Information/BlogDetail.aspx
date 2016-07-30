<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <!-- RIBBON -->
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-puzzle-piece"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>專業知識</li>
        </ol>
    </div>
    <!-- END RIBBON -->
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
    <h1 class="page-title txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-puzzle-piece"></i>專業知識
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <div class="col-sm-9">

            <div class="well well-sm bg-color-darken txt-color-white">

                <table class="table table-striped table-forum">
                    <tbody>
                        <!-- Post -->
                        <tr>
                            <td class="text-center"><strong>on <em><%= _item.Document.DocDate.ToString("yyyy-MM-dd") %></em></strong></td>
                            <td>《<%= _item.Title  %>》</td>
                        </tr>
                        <tr>
                            <td class="text-center" style="width: 12%;">
                                <div class="push-bit">
                                    <a href="profile.html">
                                        <%  if(_item.AuthorID.HasValue)
                                                _item.UserProfile.RenderUserPicture(this.Writer, "userImg"); %>
                                    </a>
                                </div>
                                <small><%: _item.AuthorID.HasValue ? _item.UserProfile.UserName : ""  %></small></td>
                            <td>
                                <p>
                                    <%= _item.ArticleContent %>
                                </p>
                                <em>- <%: _item.AuthorID.HasValue ? _item.UserProfile.UserName : ""  %>
											<br/>
                                    <%= _item.AuthorID.HasValue && _item.UserProfile.ServingCoach!=null ? _item.UserProfile.ServingCoach.Description : null %>
                                    </em>
                            </td>
                        </tr>
                        <!-- end Post -->
                    </tbody>
                </table>

            </div>
        </div>
        <div class="col-sm-3">
            <div class="well well-sm bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-cloud"></i>更多SNS</h5>
                <ul class="no-padding no-margin">
                    <p class="no-margin">
                        <a title="Facebook" href="https://www.facebook.com/beyond.fitness.pro/" target="_blank"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-facebook fa-stack-1x"></i></span></a>
                        <a title="Twitter" href="https://www.instagram.com/beyond_ft/" target="_blank"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-instagram fa-stack-1x"></i></span></a>
                    </p>
                </ul>
            </div>
            <!-- /well -->
        </div>

    </div>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    Article _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (Article)this.Model;
    }

</script>