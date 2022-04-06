<div id="offcanvas-overlay-trekhoedep" class="" uk-offcanvas="overlay: true">
    <div class="uk-offcanvas-bar uk-padding-remove">
        <!--
        <ul class="uk-nav-default trekhoedep__nav uk-nav-parent-icon" uk-nav="toggle: .trekhoedep__nav__arrow">
            <?php
            $data = array(
                array(
                    "src" => "cham-soc-da-mat.png",
                    "txt" => "CHĂM SÓC DA MẶT",
                    "submenu" => true,
                ),
                array(
                    "src" => "cham-soc-mong.png",
                    "txt" => "chăm sóc móng",
                    "submenu" => true,
                ),
                array(
                    "src" => "cham-soc-rang-mieng.png",
                    "txt" => "chăm sóc răng miệng",
                    "submenu" => true,
                ),
                array(
                    "src" => "do-dung-me-be.png",
                    "txt" => "đồ dùng mẹ & bé",
                    "submenu" => false,
                ),
                array(
                    "src" => "do-the-thao.png",
                    "txt" => "đồ thể thao",
                    "submenu" => false,
                ),
                array(
                    "src" => "may-cao-rau.png",
                    "txt" => "máy cạo râu",
                    "submenu" => false,
                ),
                array(
                    "src" => "may-massage.png",
                    "txt" => "máy massage",
                    "submenu" => false,
                ),
                array(
                    "src" => "may-tia-long-mui.png",
                    "txt" => "máy tỉa lông mũi",
                    "submenu" => false,
                ),
                array(
                    "src" => "may-triet-long.png",
                    "txt" => "máy triệt lông",
                    "submenu" => false,
                ),
                array(
                    "src" => "quan-ao-dinh-hinh.png",
                    "txt" => "quần áo định hình",
                    "submenu" => false,
                ),
            );
            foreach ($data as $k => $v): ?>
            <li class="trekhoedep__nav__li <?= ($k==0) ? 'uk-active' : '' ?> <?= ($v['submenu']) ? 'uk-parent' : '' ?>">
                <a href="#" class="trekhoedep__nav__a">
                    <div class="uk-flex-middle uk-grid-10" uk-grid>
                        <div class="uk-width-auto">
                            <div class="header_menu__danhmuc__box1__img">
                                <img src="images/menu/1x/<?= $v['src'] ?>" alt="">
                            </div>
                        </div>
                        <div class="uk-width-expand">
                            <span><?= $v['txt'] ?></span>
                        </div>
                    </div>
                    <?php if ($v['submenu']): ?>
                        <span class="trekhoedep__nav__arrow uk-position-center-right"></span>
                    <?php endif; ?>
                </a>
                <?php if ($v['submenu']): ?>
                    <ul class="uk-nav-sub">
                        <li><a href="#">Sub item</a></li>
                        <li>
                            <a href="#">Sub item</a>
                            <ul>
                                <li><a href="#">Sub item</a></li>
                                <li><a href="#">Sub item</a></li>
                            </ul>
                        </li>
                    </ul>
                <?php endif; ?>
            </li>
            <?php endforeach; ?>
        </ul>
        -->
        <ul class="uk-nav-default1 product__nav1" uk-nav="toggle: .product__icon;">
            <?php
            $data = array(
                array(
                    'txt' => 'Trang chủ',
                    'href' => '.',
                    'sub' => false,
                ),
                array(
                    'txt' => 'Menu',
                    'href' => 'menu.php',
                    'sub' => false,
                ),
                array(
                    'txt' => 'Blog',
                    'href' => 'blog.php',
                    'sub' => false,
                ),
                array(
                    'txt' => 'Liên hệ',
                    'href' => 'contact.php',
                    'sub' => false,
                ),
            );
            foreach ($data as $k => $v): ?>
                <li class="<?= ($k == 0) ? 'uk-open' : '' ?> <?= ($v['sub']) ? 'uk-parent' : '' ?>">
                    <a href="<?= $v['href'] ?>">
                        <?= $v['txt'] ?>
                        <?php if ($v['sub']): ?>
                            <span class="product__icon uk-position-right uk-background-norepeat uk-background-center-center"></span>
                        <?php endif; ?>
                    </a>
                    <?php if ($v['sub']): ?>
                        <ul class="uk-nav-sub product__nav2">
                            <?php
                            $data = array(
                                array(
                                    'txt' => 'Facebook Marketing',
                                    'href' => 'facebook-marketing.php',
                                ),
                                array(
                                    'txt' => 'Tiktok Marketing',
                                    'href' => 'tiktok-marketing.php',
                                ),
                                array(
                                    'txt' => 'Application Marketing',
                                    'href' => 'application-marketing.php',
                                ),
                                array(
                                    'txt' => 'Media Solutions',
                                    'href' => 'media-solution.php',
                                ),
                            );
                            foreach ($data as $k1 => $v1): ?>
                                <li class="<?= ($k1 == 1) ? 'uk-active' : '' ?>"><a href="<?= $v1['href'] ?>"><?= $v1['txt'] ?></a></li>
                            <?php endforeach; ?>
                        </ul>
                    <?php endif; ?>
                </li>
            <?php endforeach; ?>
        </ul>
    </div>
</div>
<script>
  // UIkit.offcanvas('#offcanvas-overlay-trekhoedep').show();
</script>