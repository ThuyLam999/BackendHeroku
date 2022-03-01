pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                 environment {
                    HEROKU_API_KEY = credentials('HEROKU_API_KEY')
                }
                bat 'HEROKU_API_KEY=%HEROKU_API_KEY% heroku container:release web --app=test-api-9'
            }           
        }

        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
