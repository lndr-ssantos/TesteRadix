<template>
  <div>
    <b-table striped :items="eventsList" :fields="tableFields"></b-table>
  </div>
</template>

<script>
import EventsApi from "@/services/EventsApi"

export default {
  name: 'EventsTable',
  data() {
    return {
      eventsList: [],
      timer: null,
      tableFields: [
        {
          key: 'tag',
          label: 'Tag'
        },
        {
          key: 'date',
          label: 'Data do evento'
        }, 
        {
          key: 'value',
          label: 'Valor'
        },
        {
          key: 'processed',
          label:'Processado com sucesso',
          formatter: 'formatProcessedValue'
        }
      ]
    }
  }, 
  methods: {
    async getEvents() {
      var eventsApi = new EventsApi();
      var events = await eventsApi.getEvents();
      this.eventsList = events.events;
    },
    formatProcessedValue(value) {
      return value ? 'Sim' : 'Não'
    },
    async updateList() {
      await new Promise(resolve => setInterval(() => resolve(this.getEvents()), 1000))
    },
    cancelAutoUpdate() {
      clearInterval(this.timer);
    }
  },
  async mounted() {
    await this.getEvents();
    this.timer = await this.updateList();
  },
  beforeDestroy() {
    this.cancelAutoUpdate();
  }
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
