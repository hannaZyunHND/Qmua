<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <title>KAO Coffee - <?= /** @var TYPE_NAME  */
        (isset($data['title'])) ? $data['title'] : ''; ?></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1">
    <!--CSS-->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- UIkit CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/css/uikit.min.css" />
    <link rel="stylesheet" href="style.css?v=<?php echo(time()) ?>">
    <!--JS-->
    <script src="https://cdn.jsdelivr.net/npm/@shinsenter/defer.js/dist/defer_plus.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <!-- UIkit JS -->
    <script src="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/js/uikit.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/uikit@3.6.18/dist/js/uikit-icons.min.js"></script>
    <script src="js/app.js"></script>
</head>
<body class="<?= ($is_home) ? 'isHome' : '' ?>">
<?php require "template-parts/commons/facebook_chat.php"; ?>
<?php require "template-parts/commons/menu-mobile.php"; ?>
<section id="app" class="uk-height-viewport uk-offcanvas-content uk-overflow-hidden uk-position-relative">
<div uk-sticky="" style="z-index: 1000;">
    <header class="header <?= ($is_home) ? 'uk-position-top uk-position-z-index' : 'uk-background-02' ?>">
        <div class="uk-container uk-padding-remove">
            <nav class="uk-navbar-container uk-navbar-transparent" uk-navbar>
                <div class="uk-navbar-left">
                    <a href="." class="uk-navbar-item uk-logo"><img src="images/logo.png" alt=""></a>
                </div>
                <div class="uk-navbar-right">
                    <ul class="uk-navbar-nav uk-visible@m">
                        <li class="uk-active"><a href=".">Trang chủ</a></li>
                        <li><a href="menu.php">Menu</a></li>
                        <li><a href="blog.php">Blog</a></li>
                        <li><a href="contact.php">Liên hệ</a></li>
                    </ul>
                    <a href="#offcanvas-overlay-trekhoedep" class="uk-navbar-toggle uk-hidden@m" uk-toggle>
                        <div id="m_nav_menu" class="m_nav menu">
                            <div class="m_nav_ham button_closed" id="m_ham_1"></div>
                            <div class="m_nav_ham button_closed" id="m_ham_2"></div>
                            <div class="m_nav_ham button_closed" id="m_ham_3"></div>
                        </div>
                    </a>
                    <div>
                        <a class="uk-navbar-toggle" href="#" uk-search-icon></a>
                        <div class="uk-navbar-dropdown" uk-drop="mode: click; cls-drop: uk-navbar-dropdown; boundary: !nav">

                            <div class="uk-grid-small uk-flex-middle" uk-grid>
                                <div class="uk-width-expand">
                                    <form class="uk-search uk-search-navbar uk-width-1-1">
                                        <input class="uk-search-input" type="search" placeholder="Search" autofocus>
                                    </form>
                                </div>
                                <div class="uk-width-auto">
                                    <a class="uk-navbar-dropdown-close" href="#" uk-close></a>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="uk-navbar-item">
                        <div class="uk-grid-5 uk-child-width-auto" uk-grid>
                            <div><a href=""><img src="images/tiengviet.jpg" alt=""></a></div>
                            <div><a href=""><img src="images/tienganh.jpg" alt=""></a></div>
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    </header>
</div>