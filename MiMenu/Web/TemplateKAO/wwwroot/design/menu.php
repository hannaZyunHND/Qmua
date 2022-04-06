<?php $data["title"] = "Menu"; ?>
<?php $is_home = false; ?>
<?php require "template-parts/layouts/header.php"; ?>
<div class="uk-background-norepeat uk-visible@m menu__section1 uk-position-relative uk-background-cover uk-light" data-src="images/05600a6dc19a37ef746fb267902398bf2.png" uk-img>
    <div class="uk-position-cover menu__overlay"></div>
    <div class="uk-position-relative uk-position-z-index uk-section-small uk-text-center">
        <div class="uk-container">
            <h1 class="uk-h1 menu__title2">Menu</h1>
            <ul class="uk-breadcrumb">
                <li><a href="">Home</a></li>
                <li><a href="">Menu</a></li>
                <li><span>Đặt hàng</span></li>
            </ul>
        </div>
    </div>
</div>
<div class="uk-section-xsmall uk-background-muted" uk-height-viewport="offset-bottom: true;offset-top: true">
    <div class="uk-container">
        <div class="uk-grid-5 uk-margin" uk-grid>
            <div>
                <span uk-icon="icon: location; ratio: 1"></span>
            </div>
            <div>
                <span class="menu__txt1">KAO Coffee, Phường Điện Bàn, Đà Nẵng</span>
            </div>
        </div>
        <div class="menu__card1 uk-margin uk-card uk-card-default uk-card-body uk-border-rounded uk-padding-small">
            <span class="menu__txt2">Nhập mã VANPHONGVUI: Giảm giá 10,000đ các sản phẩm</span>
        </div>
        <div class="uk-grid-small uk-grid-20-m" uk-grid>
            <div class="uk-width-expand">
                <div class="uk-background-default uk-border-rounded menu__content">
                    <nav uk-sticky="offset: 60;bottom: true;" class="uk-hidden@m menu__nav2 uk-navbar-container uk-navbar-transparent uk-background-default" uk-navbar>
                        <div class="nav-overlay uk-navbar-left uk-overflow-auto uk-flex-1">
                            <ul class="uk-navbar-nav uk-flex-nowrap" uk-scrollspy-nav="closest: li; scroll: true;offset: 60;" style="width: max-content">
                                <li class="uk-active"><a href="#coffee-more1">Coffee & More</a></li>
                                <li><a href="#tea-another1">Tea & Another</a></li>
                                <li><a href="#snack1">Snacks</a></li>
                                <li><a href="">Combo Phố Vui</a></li>
                                <li><a href="">Phố Giòn</a></li>
                                <li><a href="">Phố Tươi Trẻ</a></li>
                                <li><a href="">Phố Lai Rai</a></li>
                                <li><a href="">Phố Trà & Nước</a></li>
                            </ul>
                        </div>
                        <div class="nav-overlay uk-navbar-right">
                            <a class="uk-navbar-toggle" uk-search-icon uk-toggle="target: .nav-overlay; animation: uk-animation-fade" href="#"></a>
                        </div>
                        <div class="nav-overlay uk-navbar-left uk-flex-1" hidden>
                            <div class="uk-navbar-item uk-width-expand">
                                <form class="uk-search uk-search-navbar uk-width-1-1">
                                    <input class="uk-search-input" type="search" placeholder="Search" autofocus>
                                </form>
                            </div>
                            <a class="uk-navbar-toggle" uk-close uk-toggle="target: .nav-overlay; animation: uk-animation-fade" href="#"></a>
                        </div>
                    </nav>
                    <div class="uk-grid-divider uk-grid-small uk-grid-match" uk-grid>
                        <div class="uk-width-1-4@m uk-visible@m">
                            <div class="uk-padding-small uk-padding-remove-bottom uk-padding-remove-top" uk-sticky="offset: 80;">
                                <div class="uk-section-xsmall">
                                    <h3 class="uk-h3 menu__titleThucdon">Thực đơn</h3>
                                    <ul class="uk-nav uk-nav-default menu__nav1" uk-scrollspy-nav="closest: li; scroll: true;offset: 80;">
                                        <li class="uk-active"><a href="#coffee-more1">Coffee & More</a></li>
                                        <li><a href="#tea-another1">Tea & Another</a></li>
                                        <li><a href="#snack1">Snacks</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="uk-width-expand">
                            <div class="uk-padding-small menu__padding1 uk-padding-remove-bottom uk-padding-remove-top">
                                <div class="uk-section-xsmall">
                                    <div class="uk-margin uk-visible@m">
                                        <form class="uk-search uk-search-default menu__boxSearch">
                                            <span uk-search-icon></span>
                                            <input class="uk-search-input" type="search" placeholder="Tìm món nhanh">
                                        </form>
                                    </div>
                                    <div class="" uk-toggle="cls: uk-margin-medium; mode: media; media: @m" id="coffee-more1">
                                        <h3 class="uk-h3 menu__title1" id="coffee-more" uk-sticky="bottom: #tea-another;offset: 100;media: @m">Coffee & More</h3>
                                        <div class="uk-grid-small uk-grid-20-m" uk-grid>
                                            <?php for($i=0;$i<=10;$i++): ?>
                                                <div class="uk-width-1-1">
                                                    <div class="uk-card menu__card2 uk-card-default uk-card-body uk-padding-small uk-border-rounded">
                                                        <div class="uk-flex-middle" uk-grid>
                                                            <div class="uk-width-1-3 uk-width-auto@m">
                                                                <div class="menu__card2__bg">
                                                                    <img class="uk-responsive-height uk-responsive-width" src="images/mug_coffee_PNG168241.png" alt="">
                                                                </div>
                                                            </div>
                                                            <div class="uk-width-expand">
                                                                <div class="menu__card2__title">Coffee Cappuccino</div>
                                                                <div class="menu__card2__txt">VNĐ</div>
                                                                <div class="menu__card2__price menu__card2__price__old"><del>50.000đ</del></div>
                                                                <div class="menu__card2__price menu__card2__price__new">50.000đ</div>
                                                            </div>
                                                            <div class="uk-width-auto">
                                                                <a href="" class="menu__card2__link" uk-icon="icon: plus-circle; ratio: 1"></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            <?php endfor; ?>
                                        </div>
                                    </div>
                                    <div class="uk-margin-medium" id="tea-another1">
                                        <h3 class="uk-h3 menu__title1" id="tea-another" uk-sticky="bottom: #snack;offset: 100;media: @m">Tea  & Another</h3>
                                        <div class="uk-grid-small uk-grid-20-m" uk-grid>
                                            <?php for($i=0;$i<=2;$i++): ?>
                                                <div class="uk-width-1-1">
                                                    <div class="uk-card menu__card2 uk-card-default uk-card-body uk-padding-small uk-border-rounded">
                                                        <div class="uk-flex-middle" uk-grid>
                                                            <div class="uk-width-1-3 uk-width-auto@m">
                                                                <div class="menu__card2__bg">
                                                                    <img class="uk-responsive-height uk-responsive-width" src="images/mug_coffee_PNG168241.png" alt="">
                                                                </div>
                                                            </div>
                                                            <div class="uk-width-expand">
                                                                <div class="menu__card2__title">Coffee Cappuccino</div>
                                                                <div class="menu__card2__txt">VNĐ</div>
                                                                <div class="menu__card2__price menu__card2__price__old"><del>50.000đ</del></div>
                                                                <div class="menu__card2__price menu__card2__price__new">50.000đ</div>
                                                            </div>
                                                            <div class="uk-width-auto">
                                                                <a href="" class="menu__card2__link" uk-icon="icon: plus-circle; ratio: 1"></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            <?php endfor; ?>
                                        </div>
                                    </div>
                                    <div class="uk-margin-medium" id="snack1">
                                        <h3 class="uk-h3 menu__title1" id="snack" uk-sticky="offset: 100;media: @m;bottom: true;">Snacks</h3>
                                        <div class="uk-grid-small uk-grid-20-m" uk-grid>
                                            <?php for($i=0;$i<=2;$i++): ?>
                                                <div class="uk-width-1-1">
                                                    <div class="uk-card menu__card2 uk-card-default uk-card-body uk-padding-small uk-border-rounded">
                                                        <div class="uk-flex-middle" uk-grid>
                                                            <div class="uk-width-1-3 uk-width-auto@m">
                                                                <div class="menu__card2__bg">
                                                                    <img class="uk-responsive-height uk-responsive-width" src="images/mug_coffee_PNG168241.png" alt="">
                                                                </div>
                                                            </div>
                                                            <div class="uk-width-expand">
                                                                <div class="menu__card2__title">Coffee Cappuccino</div>
                                                                <div class="menu__card2__txt">VNĐ</div>
                                                                <div class="menu__card2__price menu__card2__price__old"><del>50.000đ</del></div>
                                                                <div class="menu__card2__price menu__card2__price__new">50.000đ</div>
                                                            </div>
                                                            <div class="uk-width-auto">
                                                                <a href="" class="menu__card2__link" uk-icon="icon: plus-circle; ratio: 1"></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            <?php endfor; ?>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="uk-width-1-3@m">
                <div class="uk-card uk-visible@m uk-card-default menu__card3 uk-overflow-hidden" uk-sticky="offset: 100;media: @m;bottom: true;">
                    <div class="uk-card-header">
                        <h3 class="uk-h3 menu__titleThucdon">Món chọn</h3>
                    </div>
                    <table class="uk-table uk-table-divider uk-table-small uk-table-middle uk-margin-remove">
                        <tbody>
                        <?php
                        $data = array(
                            array(
                                'count' => '1',
                                'name' => 'Coffee Capuchino',
                                'price' => '50.000đ',
                            ),
                            array(
                                'count' => '2',
                                'name' => 'Americano',
                                'price' => '50.000đ',
                            ),
                        );
                        foreach ($data as $k => $v): ?>
                        <tr>
                            <td>
                                <div class="uk-grid-5 uk-flex-middle" uk-grid>
                                    <div>
                                        <a href="#" class="uk-icon-link" uk-icon="icon:plus-circle;ratio:0.7"></a>
                                    </div>
                                    <div>
                                        <div class="uk-position-relative menu__card3__table__w1">
                                            <span class="uk-position-center menu__card3__table__txt2"><?= $v['count'] ?></span>
                                        </div>
                                    </div>
                                    <div>
                                        <a href="#" class="uk-icon-link" uk-icon="icon:minus-circle;ratio:0.7"></a>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <a href="#" class="uk-icon-link" uk-icon="icon:trash;ratio:0.7"></a>
                                <span class="uk-text-middle menu__card3__table__txt1"><?= $v['name'] ?></span>
                            </td>
                            <td>
                                <span class="menu__card3__table__txt1"><?= $v['price'] ?></span>
                            </td>
                        </tr>
                        <?php endforeach; ?>
                        <tr>
                            <td colspan="3">
                                <div class="uk-margin-small">
                                    <div class="uk-position-relative">
                                        <input class="uk-input menu__card3__box1__input uk-border-rounded" type="text" placeholder="Mã khuyến mãi của bạn">
                                        <button type="button" class="uk-button menu__card3__box1__btn uk-background-secondary uk-button-small uk-position-center-right uk-position-small uk-border-rounded">ÁP DỤNG</button>
                                    </div>
                                </div>
                                <div class="uk-margin-small">
                                    <div class="uk-alert-success uk-margin-remove" uk-alert>
                                        <a class="uk-alert-close menu__card3__box1__close" uk-close></a>
                                        <p class="menu__card3__box1__txt">Bạn đã được giảm giá 10.000đ</p>
                                    </div>
                                </div>
                                <a href="order.php" class="uk-link-toggle">
                                    <div class="menu__card3__table__btn">
                                        <div class="uk-flex-middle" uk-grid>
                                            <div class="uk-width-expand">
                                                <div class="menu__card3__submit__txt1">3 món</div>
                                                <div class="menu__card3__submit__txt2"><del>150.000đ</del></div>
                                                <div class="menu__card3__submit__txt3">140.000đ</div>
                                            </div>
                                            <div class="uk-width-auto">
                                                <span class="menu__card3__submit__txt4 uk-text-middle uk-margin-small-right">ĐẶT NGAY</span>
                                                <span uk-icon="icon: arrow-right; ratio: 1.2"></span>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </div>
                <div class="uk-hidden@m uk-position-fixed uk-position-bottom uk-padding-small uk-position-z-index">
                    <a href="order.php" class="uk-link-toggle">
                        <div class="menu__card3__table__btn">
                            <div class="uk-flex-middle" uk-grid>
                                <div class="uk-width-expand">
                                    <div class="menu__card3__submit__txt1">3 món</div>
                                    <div class="menu__card3__submit__txt2"><del>150.000đ</del></div>
                                    <div class="menu__card3__submit__txt3">140.000đ</div>
                                </div>
                                <div class="uk-width-auto">
                                    <span class="menu__card3__submit__txt4 uk-text-middle uk-margin-small-right">ĐẶT NGAY</span>
                                    <span uk-icon="icon: arrow-right; ratio: 1.2"></span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
<?php require "template-parts/layouts/footer.php"; ?>