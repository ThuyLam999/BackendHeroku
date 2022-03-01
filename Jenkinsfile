pipeline {
    agent any 

    environment {
        HEROKU_API_KEY = credentials('herokuidtest')
    }

    stages {
        stage('Push to Heroku') {
            steps {
                withEnv(['PATH+HEROKU=C:\\Program Files\\heroku\\bin']) {
                    bat 'heroku container:release web --app=test-api-9'
                }
            }          
        }
    
        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
