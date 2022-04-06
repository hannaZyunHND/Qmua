<?php $data["title"] = "Blog detail"; ?>
<?php $is_home = false; ?>
<?php require "template-parts/layouts/header.php"; ?>

<div class="uk-section blog-detail__section1 uk-background-norepeat uk-background-top-center" data-src="images/blog-detail/anhchim1.png" uk-img>
    <div class="uk-container uk-container-small mb-15 mb-33-m">
        <ul class="uk-breadcrumb mb-15 mb-55-m">
            <li><a href="">Trang Chủ</a></li>
            <li><a href="">Bài Viết Mới</a></li>
            <li><span>Ác mộng Covid-19 trở lại, nhiều nước rục rịch phong tỏa</span></li>
        </ul>
        <div class="uk-flex-middle uk-child-width-auto mb-15 mb-60-m" uk-grid>
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
        <h1 class="uk-h1 uk-text-center blog-detail__title mb-20 mb-40-m">Ác mộng Covid-19 trở lại, nhiều nước rục rịch phong tỏa</h1>
        <div class="uk-text-center blog-detail__desc mb-20 mb-40-m">Nhiều nước châu Âu chuẩn bị áp đặt các biện pháp phong tỏa nghiêm ngặt, nhằm kiểm soát làn sóng dịch bệnh lần ba.</div>
        <article class="uk-article blog-detail__article mb-20 mb-40-m">
            <figure>
                <img src="https://znews-photo.zadn.vn/w660/Uploaded/seaekt/2021_03_14/ap.jpeg" alt="">
                <figcaption>Đường phố thủ đô Paris, Pháp, trong thời đại dịch. Ảnh: US News.</figcaption>
            </figure>
            <p>Chỉ trong ngày 12/3, giới chức Italy ghi nhận thêm 27.000 bệnh nhân Covid-19 và 389 trường hợp tử vong. Thủ tướng Mario Draghi tuyên bố: “Hơn một năm trải qua cuộc khủng hoảng sức khỏe, chúng ta lại phải đối mặt với một làn sóng lây nhiễm mới”.</p>
            <p>“Ký ức về thảm kịch năm ngoái vẫn còn đó. Chúng tôi sẽ làm mọi thứ để ngăn điều này lặp lại”, Guardian dẫn lời ông Draghi.</p>
            <p>Từ ngày 15/3, phần lớn địa phương ở Italy sẽ phong tỏa nghiêm ngặt và người dân chỉ được ra ngoài khi có nhu cầu thiết yếu. Tại đây, hầu hết cửa hàng, quán bar và nhà hàng cũng phải đóng cửa.</p>
        </article>
        <div class="uk-section-xsmall blog-detail__box1 mb-20 mb-59-m">
            <div class="uk-flex-middle uk-grid-small uk-grid-69-m" uk-grid>
                <div class="uk-width-auto@m">
                    <div class="uk-child-width-auto uk-grid-small uk-grid-26-m" uk-grid>
                        <div><a href=""><img src="images/blog-detail/instagram.png" alt=""></a></div>
                        <div><a href=""><img src="images/blog-detail/facebook.png" alt=""></a></div>
                        <div><a href=""><img src="images/blog-detail/twiter.png" alt=""></a></div>
                        <div><a href=""><img src="images/blog-detail/tiktok.png" alt=""></a></div>
                    </div>
                </div>
                <div class="uk-width-expand">
                    <div class="uk-child-width-auto uk-grid-small uk-grid-56-m" uk-grid>
                        <div>
                            <div class="uk-flex-middle uk-grid-7" uk-grid>
                                <div class="uk-width-auto">
                                    <img src="images/blog-detail/icon/action/visibility_24px.png" alt="">
                                </div>
                                <div class="uk-width-expand">
                                    <span class="blog-detail__box1__txt">1.258</span>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="uk-flex-middle uk-grid-7" uk-grid>
                                <div class="uk-width-auto">
                                    <img src="images/blog-detail/icon/editor/mode_comment_24px.png" alt="">
                                </div>
                                <div class="uk-width-expand">
                                    <span class="blog-detail__box1__txt">45</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="uk-width-auto">
                    <div class="uk-flex-middle uk-grid-7" uk-grid>
                        <div class="uk-width-auto">
                            <img src="images/blog-detail/icon/content/reply_24px.png" alt="">
                        </div>
                        <div class="uk-width-expand">
                            <a href="" class="blog-detail__box1__link">Chia Sẻ</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <h2 class="uk-h2 blog-detail__title1 uk-margin-remove">Bài Viết Liên Quan</h2>
    </div>
    <div class="uk-container">
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
    </div>
</div>




<?php require "template-parts/layouts/footer.php"; ?>