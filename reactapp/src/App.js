import React, {Component} from 'react'

import Alert from './Alert'
import Form from './Form'
import Forward from './Forward'


class App extends Component {
    state = {redirectUrl:''}
    render() {
        if(window.location.pathname == '/')
            return (
                <div className="App">
                    <Alert
                        updateRedirectUrl={this.updateRedirectUrl}
                        redirectUrl={this.state.redirectUrl} />
                    <Form updateRedirectUrl={this.updateRedirectUrl} />
                </div>
            )

        return (
            <div className="App">
                <Forward
                shortUrl={window.location.pathname} />
            </div>
        )
    }

    updateRedirectUrl = (redirectUrl) => {
        this.setState({
            redirectUrl: redirectUrl
        })
    }
}

export default App