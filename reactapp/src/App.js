import React, {Component} from 'react'

import Alert from './Alert'
import Form from './Form'


class App extends Component {
    state = {url:'',name:'', redirectUrl:''}
    render() {
        const {redirectUrl} = this.state
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