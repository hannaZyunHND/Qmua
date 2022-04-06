<?php $data["title"] = "Blog"; ?>
<?php $is_home = false; ?>
<?php require "template-parts/layouts/header.php"; ?>



<div class="blog__section1 uk-section uk-background-norepeat uk-background-top-center" data-src="images/bg2.png" uk-img>
    <div class="uk-container">
        <div class="uk-child-width-auto uk-grid-small uk-grid-41-m uk-flex-center mb-20 mb-56-m" uk-grid>
            <?php
            $data = array(
                'Tất cả',
                'Bài viết mới',
                'Tư vấn',
                'Tuyển dụng',
            );
            foreach ($data as $k => $v): ?>
            <div>
                <a href="" class="<?= ($k==0) ? 'uk-active' : '' ?> blog__btn uk-button uk-button-default uk-button-large"><?= $v ?></a>
            </div>
            <?php endforeach; ?>
            <div class="uk-width-expand">
                <form class="uk-search uk-search-default uk-width-1-1">
                    <span class="uk-search-icon-flip blog__boxSearch__icon uk-background-norepeat uk-background-center-center" data-src="images/Group-iconsearch.png" uk-img uk-search-icon></span>
                    <input class="uk-search-input uk-form-large uk-width-1-1 blog__boxSearch__inputSearch" type="search" placeholder="Tìm Kiếm">
                </form>
            </div>
        </div>
        <div class="uk-child-width-1-3@m mb-20 mb-40-m uk-grid-small uk-grid-20-m blog__grid" uk-grid>
            <?php
            function to_slug($str) {
                $str = trim(mb_strtolower($str));
                $str = preg_replace('/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/', 'a', $str);
                $str = preg_replace('/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/', 'e', $str);
                $str = preg_replace('/(ì|í|ị|ỉ|ĩ)/', 'i', $str);
                $str = preg_replace('/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/', 'o', $str);
                $str = preg_replace('/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/', 'u', $str);
                $str = preg_replace('/(ỳ|ý|ỵ|ỷ|ỹ)/', 'y', $str);
                $str = preg_replace('/(đ)/', 'd', $str);
                $str = preg_replace('/[^a-z0-9-\s]/', '', $str);
                $str = preg_replace('/([\s]+)/', '-', $str);
                return $str;
            }
            $data = array(
                array(
                    'src' => 'images/blog/img1.png',
                    'txt1' => 'Bài Viết Mới',
                ),
                array(
                    'src' => 'images/blog/img2.png',
                    'txt1' => 'Tư Vấn',
                ),
                array(
                    'src' => 'images/blog/img3.png',
                    'txt1' => 'Tuyển Dụng',
                ),
                array(
                    'src' => 'images/blog/img4.png',
                    'txt1' => 'Bài Viết Mới',
                ),
                array(
                    'src' => 'images/blog/img5.png',
                    'txt1' => 'Tư Vấn',
                ),
                array(
                    'src' => 'images/blog/img6.png',
                    'txt1' => 'Tuyển Dụng',
                ),
                array(
                    'src' => 'images/blog/img1.png',
                    'txt1' => 'Bài Viết Mới',
                ),
                array(
                    'src' => 'images/blog/img2.png',
                    'txt1' => 'Tư Vấn',
                ),
                array(
                    'src' => 'images/blog/img3.png',
                    'txt1' => 'Tuyển Dụng',
                ),
            );
            foreach ($data as $k => $v): ?>
            <div>
                <div class="uk-cover-container blog__box1__bg">
                    <img src="<?= $v['src'] ?>" alt="" uk-cover>
                    <canvas width="720" height="582"></canvas>
                    <div class="uk-display-inline-block uk-position-bottom-right blog__box1__catalog <?= to_slug($v['txt1']) ?>"><span><?= $v['txt1'] ?></span></div>
                </div>
                <div>
                    <div class="uk-flex-middle uk-child-width-auto uk-flex-between uk-grid-collapse mb-10 mb-20-m" uk-grid>
                        <div>
                            <div class="uk-child-width-auto uk-flex-middle uk-grid-12" uk-grid>
                                <div>
                                    <div class="uk-cover-container uk-border-circle">
                                        <img src="images/lemanhhai_anhthe.jpg" alt="" uk-cover>
                                        <canvas width="25" height="25"></canvas>
                                    </div>
                                </div>
                                <div>
                                    <span class="blog__box1__name">Cooker ADM</span>
                                </div>
                            </div>
                        </div>
                        <div>
                            <ul class="uk-child-width-auto uk-grid-27 blog__subnav" uk-grid>
                                <li><span class="blog__box1__name">13 ngày trước</span></li>
                                <li><span class="blog__box1__name">1257 views</span></li>
                            </ul>
                        </div>
                    </div>
                    <h5 class="uk-h5 blog__box1__title uk-margin-remove"><a href="blog-detail.php">Lorem Ipsum is simply dummy text of the printi and typesetting industry. Lorem Ipsum has been</a></h5>
                </div>
            </div>
            <?php endforeach; ?>
        </div>
        <?php require "template-parts/layouts/pagination1.php"; ?>
        <ul class="uk-pagination uk-flex-center uk-grid-12" uk-grid uk-margin>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#"><span uk-pagination-previous></span></a></li>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#">1</a></li>
            <li class="uk-disabled uk-pagination__li"><span class="uk-pagination__a">...</span></li>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#">5</a></li>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#">6</a></li>
            <li class="uk-active uk-pagination__li"><span class="uk-pagination__a">7</span></li>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#">8</a></li>
            <li class="uk-pagination__li"><a class="uk-pagination__a" href="#"><span uk-pagination-next></span></a></li>
        </ul>
    </div>
</div>


<?php require "template-parts/layouts/footer.php"; ?>