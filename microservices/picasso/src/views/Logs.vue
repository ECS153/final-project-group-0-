<template>
  <div id="home">
    <transition name="slide-fade" mode="out-in">
      
    </transition>
  </div>
</template>

<script>
/* eslint-disable */
import "@/assets/css/views/logs.css";


export default {
  name: "Home",
  components: {

  },
  data() {
    return {
      requestSwap: null
    };
  },
  methods: {
    refresh: function() {
      this.$http
        .request({
          url: "http://localhost:8000/logs/",
          method: "get",
          headers: this.$apiHeaders
        })
        .then(resp => {
          
        })
        .catch ((err) => {
          if (err.response.status == 401) {
            this.$toast.error("Unauthorized");
          } else {
            this.$toast.error(err.response.data.title);
          }
      });
    }
  }
};
</script>
