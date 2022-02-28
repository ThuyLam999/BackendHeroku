pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                withEnv(['HEROKU=C:\\Program Files\\heroku\\bin']) {
                    dir('HEROKU\\heroku')
                    {
                        bat 'heroku container:release web --app=test-api-9'
                    }
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
