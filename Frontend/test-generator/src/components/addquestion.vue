<template>
<div>
    <h2>Question statement</h2>

<b-container fluid>
  <b-row class="mt-2">
    <b-col sm="3">
    </b-col>
    <b-col sm="8">
      <b-form-textarea id="textarea" v-model="message" placeholder="Write the question statement here..." rows="1" max-rows="6" ></b-form-textarea>
    </b-col>
    <b-col sm="3">
    </b-col>
  </b-row>
</b-container>


<br>
    <!-- <textarea v-model="question" placeholder="Write the question statement here..."></textarea> -->
    <h2>Answers</h2>

    <div v-for="n in index" :key="n.id">
        
        
        <b-container fluid>
            <b-row class="mt-2">
                <b-col sm="3">
                    
                </b-col>
                <b-col sm="8">
                    <b-row>
                        <b-col sm="1">
                            <b-form-radio type="radio" :id="n" :value="n" v-model="picked"></b-form-radio>
                        </b-col>
                        <b-col sm=10>
                            <b-form-textarea v-model="alternatives[n-1]" placeholder="Write the answer here..." rows="1" max-rows="6" ></b-form-textarea>
                        </b-col>
                        <b-col sm="1">
                            <b-button pill variant="danger" v-on:click="minus(n)">-</b-button>
                        </b-col>
                    </b-row>
                    
                    
                </b-col>
                <b-col sm="3">
                </b-col>
            </b-row>
        </b-container>

        <!-- <textarea v-model="alternatives[n-1]" placeholder="Write the answer here..."></textarea> -->
        
    </div>
    <br>

    <b-button pill variant="primary" v-on:click="plus">+</b-button>
        
<br>
    <br>
    <h2>Difficulty</h2>
    <b-form-group>
      <b-form-radio-group
        id="btn-radios-1"
        v-model="selected"
        :options="['Easy','Medium','Hard']"
        buttons
        name="radios-btn-default"
      ></b-form-radio-group>
    </b-form-group>
    <br>
    
    <b-button-group class="mx-1">
    <b-button variant="success" v-on:click="save">Save</b-button>

    </b-button-group>
    <b-button-group class="mx-1">
    <b-button variant="danger" v-on:click="reset">Reset</b-button>

    </b-button-group>
    
    

</div>
</template>

<script>
    import axios from 'axios';
    import VueFetch from 'vue-fetch'
    import Vue from 'vue'

    export default {
        name: 'AddQuestions',
        inputs:
        {
            name: ''
        },
        components: {
            
        },
        data() {
            return {
                message: '',
                alternatives: ['',''],
                index: 2,
                picked: 1,
                selected: 'Easy'
            }
        },
        methods:
        {
            plus: function ()
            {
                this.index += 1;
                this.alternatives.push('')
            },
            minus: function (n)
            {
                if(this.index > 2)
                {
                    this.alternatives.splice(n-1,1);
                    if(this.picked == n)
                    {
                        this.picked = 1;
                    }

                    this.index -= 1;
                }
            },
            reset: function ()
            {
                this.message = '';
                this.alternatives = ['',''];
                this.index = 2;
                this.picked = 1;
                this.selected = 'Easy';
            },
            save: function ()
            {
                var send = {};
                send.content = this.message;
                send.difficulty = this.selected;
            
                var msgs = [];
                var i = 0;
                this.alternatives.forEach(element => {
                    var aux = {};
                    aux.content = element;
                    if(this.picked == i+1)
                    {
                        aux.isCorrect = true;
                    }
                    else
                    {
                        aux.isCorrect = false;    
                    }
                    msgs.push(aux);
                    i++;
                });
                send.answers = msgs;
                console.log(send);
                
                Vue.use(VueFetch, {
                polyfill: true   //should vue-fetch load promise polyfill, set to false to use customer polyfill
                });

                console.log(JSON.stringify(send));
                const vm = new Vue({
                });
                (async function(){
                let login = await vm.$fetch.post('http://10.160.47.210:5001/api/quiz/question', send);
                if (login.status != 200) {
                    alert('login error');
                }

                else
                {
                    alert("something");
                }
                // if (user.status != 200){
                //     alert('can not get profile')
                // }
            })()

                axios.defaults.crossDomain = true;
                axios.defaults.preflightContinue = true;

                axios("http://10.160.47.210:5001/api/quiz/question", {
                method: 'POST',
                mode: 'no-cors',
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Content-Type': 'application/json',
                },
                withCredentials: true,
                credentials: 'same-origin',
                }).then(response => {
                    console.log(response);
                })

                axios
                .post('http://10.160.47.210:5001/api/quiz/question', send )
                // .post('http://195.80.130.120:9000/api/quiz/question', {data: send})
                .then(
                    response =>
                    {
                        alert("SAVED")
                    }
                )
                // .catch(function (error) {
                //     console.log(error);
                // });
            },
            
        }
    }
</script>