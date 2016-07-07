<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="navbar navbar-default navbar-top">
    <div class="container">
        <div class="navbar-header">
            <!-- Stat Toggle Nav Link For Mobiles -->
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <i class="fa fa-bars"></i>
            </button>
            <!-- End Toggle Nav Link For Mobiles -->
            <a class="navbar-brand hidden-xs" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">
                <img src="<%= VirtualPathUtility.ToAbsolute("~/images/Beyond-Fitness.png") %>" alt=""></a>
            <a class="navbar-brand visible-xs-block" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">
                <img src="<%= VirtualPathUtility.ToAbsolute("~/images/Beyond-Fitness.png") %>" alt="" width="270" height="41"></a>
        </div>
        <div class="navbar-collapse collapse">
            <!-- Start Navigation List -->
            <ul class="nav navbar-nav navbar-right">
                <li><a id="home" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                <li><a id="professional" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional") %>">專業體能訓練</a></li>
                <li><a id="blog" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Blog") %>">專業知識</a></li>
                <li><a id="rental" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Rental") %>">場地租借</a></li>
                <li><a id="products" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Products") %>">相關商品</a></li>
                <li><a id="cooperation" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Cooperation") %>">相關合作</a></li>
                <li><a id="contactUs" href="<%= VirtualPathUtility.ToAbsolute("~/Information/ContactUs") %>">聯絡我們</a></li>
                <%  if (_userProfile == null)
                    { %>
                <li><a id="vip" href="<%= FormsAuthentication.LoginUrl %>">會員專區</a></li>
                <%  }
                    else
                    { %>
                        <li><a id="vip" href="#">會員專區</a>
                            <ul class="dropdown">
                        <%--<li><a id="login" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Login") %>">登入</a>
                                        </li>--%>
                        <%  if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Coach || _userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.FreeAgent)
                            { %>
                                <li><a id="coach" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>">課程管理</a>
                                </li>
                                <li><a id="member" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">人員管理</a>
                                </li>
                                <li><a id="pub" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Publish") %>">知識上稿</a>                        
                                </li>
                        <%  }
                            else if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Learner)
                            { %>
                                <li><a id="vip" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Vip") %>">會員專區</a></li>
                        <%  }
                            else if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Administrator)
                            {  %>
                                <li><a id="coach" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>">課程管理</a>
                                </li>
                                <li><a id="member" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">人員管理</a>
                                </li>
                                <li><a id="pub" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Publish") %>">知識上稿</a>
                                </li>
                                <li><a id="vip" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Vip") %>">會員專區</a></li>
                        <%  } %>
                            </ul>
                        </li>
                        <li><a id="logout" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Logout") %>">登出</a></li>
                <%  } %>
            </ul>
            <!-- End Navigation List -->
        </div>
    </div>

    <!-- Mobile Menu Start -->
    <ul class="wpb-mobile-menu">
        <li><a id="m_home" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
        <li><a id="m_professional" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional") %>">專業體能訓練</a></li>
        <li><a id="m_blog" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Blog") %>">專業知識</a></li>
        <li><a id="m_rental" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Rental") %>">場地租借</a></li>
        <li><a id="m_products" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Products") %>">相關商品</a></li>
        <li><a id="m_cooperation" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Cooperation") %>">相關合作</a></li>
        <li><a id="m_contactUs" href="<%= VirtualPathUtility.ToAbsolute("~/Information/ContactUs") %>">聯絡我們</a></li>
        <%  if (_userProfile == null)
                    { %>
        <li><a id="m_vip" href="<%= FormsAuthentication.LoginUrl %>">會員專區</a></li>
        <%  }
                    else
                    { %>
        <li><a id="m_vip" href="#">會員專區</a>
            <ul class="dropdown">
                <%--<li><a id="m_login" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Login") %>">登入</a>
                                        </li>--%>
                <%  if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Coach || _userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.FreeAgent)
                            { %>
                <li><a id="m_coach" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>">課程管理</a>
                </li>
                <li><a id="m_member" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">人員管理</a>
                </li>
                <li><a id="m_pub" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Publish") %>">知識上稿</a>
                </li>
                <%  }
                            else if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Learner)
                            { %>
                <li><a id="m_vip" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Vip") %>">會員專區</a></li>
                <%  }
                            else if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Administrator)
                            {  %>
                <li><a id="m_coach" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>">課程管理</a>
                </li>
                <li><a id="m_member" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">人員管理</a>
                </li>
                <li><a id="m_pub" href="<%= VirtualPathUtility.ToAbsolute("~/Information/Publish") %>">知識上稿</a>
                </li>
                <li><a id="m_vip" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Vip") %>">會員專區</a></li>
                <%  } %>
            </ul>
        </li>
        <li><a id="m_logout" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Logout") %>">登出</a></li>
        <%  } %>
    </ul>
    <!-- Mobile Menu End -->

</div>


<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _userProfile = Context.GetUser();
    }

</script>
