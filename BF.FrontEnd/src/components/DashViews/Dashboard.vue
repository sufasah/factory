<template>
  <v-container
    fluid
    grid-list-xl
  >
    <v-layout 
      wrap
    >   
      <v-flex
        sm12
        xs12
        md6
        lg6
      >
          <apexchart 
            ref="c1"
            height="300"
            :width="chartWidths"
            type="donut" 
            :options="chartOptions" 
            :series="series"
          >
          </apexchart>
      </v-flex>
      <v-flex
        sm12
        xs12
        md6
        lg6
        
      >
        <apexchart 
          ref="c2"
          height="300" 
          :width="chartWidths"
          type="donut" 
          :options="workcenterChartOptions" 
          :series="seriesWorkcenter"
        >

        </apexchart>
      </v-flex>
      
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="green"
          icon="mdi-view-list"
          title="Total Orders"
          :value="cardValues[0]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="orange"
          icon="mdi-wallet-giftcard"
          title="Total Products"
          :value="cardValues[1]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="red"
          icon="mdi-sitemap"
          title="Total Sub Products"
          :value="cardValues[2]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="info"
          icon="mdi-wrench"
          title="Total Workcenters"
          :value="cardValues[3]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="general"
          icon="mdi-vector-polyline"
          title="Total Operations"
          :value="cardValues[4]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="yellow"
          icon="mdi-bulletin-board"
          title="Total Workcenter Operations"
          :value="cardValues[5]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="purple"
          icon="mdi-calendar-clock"
          title="Total Schedules"
          :value="cardValues[6]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>
      <v-flex
        sm6
        xs12
        md6
        lg3
      >
        <material-stats-card
          color="pink"
          icon="mdi-account-alert"
          title="Orderd Will Not Be Ready At Its Deadline"
          :value="cardValues[7]"
          sub-icon="mdi-factory"
          sub-text="My Factory"
        />
      </v-flex>

<!-- 
  ------------------------------------------ SNACKBAR
-->

    <v-snackbar
        v-model="snackbar.show"
        :color="snackbar.color"
        :bottom="true"
      >
        {{ snackbar.msg }}
        <v-btn
          light
          flat
          @click="snackbar.show = false"
        >
          Close
        </v-btn>
    </v-snackbar>    
    </v-layout>
  </v-container>
</template>

<script>
import VueApexCharts from "vue-apexcharts"
export default {
  name: 'Dashboard',
  components:{apexchart:VueApexCharts},
  data: () => ({
    series: [],
    chartOptions: {
      labels: ['Ordered','Orders In Queue','Orders Delivered','Orders Cancelled'],
      chart: {
        type: 'donut',
        background: '#424242'
      },
      legend: {
          show: true,
          floating: false,
          fontSize: '14px',
          fontFamily: 'Helvetica, Arial',
          fontWeight: 400,
          labels: {
              useSeriesColors: true
          },
      }
    },
    chartWidths:"0",
    snackbar:{
      show:false,
      color:"",
      msg:""
    },
    seriesWorkcenter: [],
    workcenterChartOptions: {
      labels: ['Active Workcenters','Not Active Workcenters'],
      chart: {
        type: 'donut',
        background: '#424242'
      },
      legend: {
          show: true,
          floating: false,
          fontSize: '14px',
          fontFamily: 'Helvetica, Arial',
          fontWeight: 400,
          labels: {
              useSeriesColors: true
          },
      }
    },
    cardValues:{
      0:"-2",
      1:"-1",
      2:"0",
      3:"1",
      4:"2",
      5:"3",
      6:"4",
      7:"5",
    }
  }),
  created (){
    
  },
  mounted () {
    this.chartWidths="100%"
    this.$store.dispatch("getOrdersGroupedByState")
    .then((result) => {
      const d = result.data.data
      this.series = [
        d.ordered,
        d.orderInQueue,
        d.orderDelivered,
        d.orderCancelled
      ]
    }).catch((err) => {
      this.showError("Orders could not be fetched by state.")
    });

    this.$store.dispatch("getWorkcentersGroupedByActive")
    .then((result) => {
      const d = result.data.data
      this.seriesWorkcenter = [
        d.active,
        d.notActive
      ]
    }).catch((err) => {
      this.showError("Worcenters could not be fetched by active.")
    });

    this.getCount("orders",0)
    this.getCount("products",1)
    this.getCount("subproducts",2)
    this.getCount("workcenters",3)
    this.getCount("operations",4)
    this.getCount("workcenteroperations",5)
    this.getCount("schedules",6)
    this.getCount("orders",7,"dashboard-deadline")
  },
  methods: {
    showError(msg){
      this.snackbar.color="red"
      this.snackbar.msg="Server error occured. Check response from network. "+msg
      this.snackbar.show = true
    },
    getCount(table,key,func){
      this.$store.dispatch("getCount",{table:table,func:!func?"count":func})
      .then((result) => {
        this.cardValues[key] = String(result.data.data)
      }).catch((err) => {
        this.showError(table+" count could not be fetched.")
      });
    }
  }
}
</script>

<style scoped lang="css">
  .flex {
    margin-top:50px;
  }
</style>