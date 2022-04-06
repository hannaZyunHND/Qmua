<template>
    <div style="margin-top:15px">
        <b-form-group >
            <b-form-file id="file-field" size="sm" v-on:change="updatePreview"></b-form-file>

            <img style="margin-top:10px" v-bind:data="imagePreview"  :src="img"  class="preview-image img-thumbnail" v-on:click="openUpload">
        </b-form-group>
        
    </div>
</template>

<script>
    export default {
        data() {
            return {
                img: this.imagePreview,
            }
        },
        props: {
            imagePreview: {
                type: String,
                default: '/assets/img/unnamed.jpg',
                required: true
            }
        },
        methods: {
            openUpload() {
                document.getElementById('file-field').click()
            },
            updatePreview(e) {
                console.log('e', e)
                var reader, files = e.target.files
                if (files.length === 0) {
                    console.log('Empty')
                }
                reader = new FileReader();
                reader.onload = (e) => {
                    this.img = e.target.result
                   
                }
                reader.readAsDataURL(files[0])
            }
        },
        watch: {
            imagePreview: function (newVal) {
                this.img = newVal;
                
            }
        }
    }
</script>

<style>
    .preview-image {
        width: 327px;
        height: 184px;
    }

    .fileUpload {
        position: relative;
        overflow: hidden;
        margin: 10px;
        margin-left:0;
    }

        .fileUpload input.upload {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            padding: 0;
            font-size: 20px;
            cursor: pointer;
            opacity: 0;
            filter: alpha(opacity=0);
        }
</style>
