import React, {Component} from 'react'

import Alert from './Alert'
import Form from './Form'


class App extends Component {
    state = {url:'',name:'', redirectUrl:''}
    render() {
        const {redirectUrl} = this.state

        //Check to see if we should be forwarding somewhere first.
        var pathname = window.location.pathname;
        if(pathname != '/')
        {
          fetch('/api'+pathname,
            {
                method: 'GET'
            })
            .then(response => response.json())
            .then(data => {
                window.location.replace(data.url)
            });

            return (
            <div className="alert alert-primary alert-dismissible fade show col-md-6 mx-auto" role="alert">
                <strong>You are being redirected. Your site will load shortly.</strong>
            </div>
            )
        }

        return (
            <div className="App">
                <Alert renderResult={this.renderResult} redirectUrl={this.state.redirectUrl} />
                <Form renderResult={this.renderResult} />
            </div>
        )
    }

    renderResult = (redirectUrl) => {
        this.setState({redirectUrl: redirectUrl})
    }
}

export default App