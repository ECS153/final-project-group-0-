<template>
    <button @click="onClick" :class="{rotate: clicked}"></button>
</template>

<script>
export default {
    name: 'RefreshButton',
    data() {
        return {
            clicked: false,
        }
    },
    methods: {
        /* Add clicked attribute, and remove after 1 second */
        onClick: function() {
            this.clicked = true;
            this.$emit('click'),
            setTimeout(()=> {
                this.clicked = false
            }, 1000)
        }
    }
}
</script>

<style scoped>
button {
    width: 150px;
    height: 150px;
    outline: none;
    border:none;
    border-radius: 100%;
    border: solid 10px #00e676;
box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);

    background: url("~@/assets/refresh.png") #00e676;
    background-size: 85%;
    background-repeat: no-repeat;
    background-position: center;
}
.rotate {
    transform: rotate(720deg);
    transition: all 1s ease-in-out;
}

.box-shadow-2 {
	
}
</style>