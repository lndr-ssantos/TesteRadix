<template>
  <div>
    <p>aqui graficos</p>
  </div>
</template>

<script>
import EventsApi from "@/services/EventsApi"

export default {
  name: 'Chart',
  data() {
    return {
      
    }
  }, 
  methods: {
    async getEvents() {
      var eventsApi = new EventsApi();
      var events = await eventsApi.getEvents();
      this.eventsList = events.events;

      console.log(this.eventsList);
    },
    formatProcessedValue(value) {
      return value ? 'Sim' : 'Não'
    }
  }//,
//   async mounted() {
//     await this.getEvents();
//   }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
