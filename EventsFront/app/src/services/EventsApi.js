import axios from 'axios'

export default class EventsApi {
    async getEvents() {
        var url = 'http://localhost:5000/api/events';
        var response = await axios.get(url);

        return response.data;
    }

    async getEventsProcessed() {
        var url = 'http://localhost:5000/api/events/processed';
        var response = await axios.get(url);

        return response.data;
    }
}