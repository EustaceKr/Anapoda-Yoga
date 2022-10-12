<template>
    <div v-if="visible" class="modal-background">
        <div class="modal-content" :style="modalState">
            <div class="m-header">
                <slot name="header">
                    <h5 class="d-flex align-items-center">{{ title }}</h5>
                </slot>
                <button @click="hide" class="btn py-0 close">&times;</button>
            </div>

            <div>
                <slot />
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        width: { type: String, default: "fit-content" },
        maxWidth: { type: String, default: "90%" },
        height: { type: String, default: "auto" },
        title: { type: String, default: "" },
    },
    data() {
        return {
            visible: false
        }
    },
    computed: {
        modalState() {
            return {
                width: this.width,
                height: this.height,
                maxWidth: this.maxWidth,
                maxHeight: "90%",
                overflow: "auto"
            };
        }
    },
    methods: {
        hide() { this.$emit("onHide"); this.visible = false; },
        show() { this.visible = true; }
    },
    created() {
        if ("visible" in this.$attrs)
            this.visible = true;
    }
}
</script>

<style scoped>
.modal-background {
    display: flex;
    position: fixed;
    z-index: 999;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0, 0, 0);
    background-color: rgba(0, 0, 0, 0.4);
}

.m-header h4 {
    display: flex;
    align-items: flex-end;
}

    .m-header {
        margin-top: .5rem;
        display: flex;
        border-bottom: 1px solid #eeeeee;
        align-items: baseline;
        margin-bottom: 1rem;
    }

.modal-content {
    background-color: #fefefe;
    margin: auto;
    padding: 0 1rem 1rem 1rem;
    border: 1px solid #888;
}

.close {
    color: #aaa;
    font-size: 28px;
    font-weight: bold;
    margin-left: auto;
    transition: all .2s ease-in-out;
}

.close:hover,
.close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
    transform: scale(1.15)
}
</style>