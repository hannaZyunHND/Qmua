<?php $data["title"] = "Contact"; ?>
<?php $is_home = false; ?>
<?php require "template-parts/layouts/header.php"; ?>
<div class="uk-section-small contact__section1">
    <div class="uk-container">
        <ul class="uk-breadcrumb">
            <li><a href="">Trang Chủ</a></li>
            <li><span>Liên Hệ</span></li>
        </ul>
        <div class="uk-child-width-1-2@m uk-grid-match" uk-grid>
            <div>
                <div class="uk-cover-container">
                    <iframe class="uk-position-cover" src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d7447.577136552509!2d105.78954723271849!3d21.04114428633194!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1svi!2ssg!4v1616985270673!5m2!1svi!2ssg" width="100%" height="100%" style="border:0;" allowfullscreen="" loading="lazy"></iframe>
                    <canvas width="1920" height="1080"></canvas>
                </div>
            </div>
            <div>
                <form class="contact__boxForm">
                    <fieldset class="uk-fieldset">

                        <legend class="uk-legend contact__boxForm__title uk-text-center uk-text-uppercase">liÊN HỆ</legend>
                        <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid">
                            <label><input class="uk-radio" type="radio" name="radio2" checked> <span class="contact__boxForm__txtRadio">Anh</span></label>
                            <label><input class="uk-radio" type="radio" name="radio2"> <span class="contact__boxForm__txtRadio">Chị</span></label>
                        </div>
                        <div class="uk-margin">
                            <input class="uk-input uk-border-pill contact__boxForm__input" type="text" placeholder="Họ và tên (bắt buộc)">
                        </div>
                        <div class="uk-margin">
                            <input class="uk-input uk-border-pill contact__boxForm__input" type="text" placeholder="Số điện thoại">
                        </div>
                        <div class="uk-margin">
                            <input class="uk-input uk-border-pill contact__boxForm__input" type="text" placeholder="Email">
                        </div>
                        <div class="uk-margin">
                            <textarea class="uk-textarea uk-border-rounded contact__boxForm__input" rows="5" placeholder="Nội dung chi tiết"></textarea>
                        </div>
                        <div class="uk-text-center">
                            <button type="submit" class="uk-button contact__boxForm__btn uk-button-secondary uk-border-pill">GỬI</button>
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
</div>
<div class="uk-section-large contact__section2 uk-background-norepeat uk-background-top-center" data-src="images/Frame-bgcontact.png" uk-img>
    <div class="uk-container uk-container-small">
        <div class="uk-child-width-1-3@m uk-grid-match" uk-grid>
            <?php
            $data = array(
                array(
                    'src' => 'images/zalo1.png',
                    'txt' => 'Tất cả những gì bạn cần biết về 
THE KAO COFFEE
và cách chúng tôi giúp khách hàng phát triển',
                ),
                array(
                    'src' => 'images/fb1.png',
                    'txt' => '@kaocoffee',
                ),
                array(
                    'src' => 'images/all1.png',
                    'txt' => 'Hỗ trợ trực tuyến 24/7
365 ngày/năm',
                ),
            );
            foreach ($data as $k => $v): ?>
            <div>
                <div class="uk-flex uk-flex-column uk-text-center">
                    <div class="mb-20 mb-45-m">
                        <img src="<?= $v['src'] ?>" alt="">
                    </div>
                    <div class="uk-flex-1 uk-flex uk-flex-middle uk-flex-center">
                        <div class="contact__boxSocial__txt">
                            <?= $v['txt'] ?>
                        </div>
                    </div>
                </div>
            </div>
            <?php endforeach; ?>
        </div>
    </div>
</div>
<?php require "template-parts/layouts/footer.php"; ?>