pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                withEnv(['HEROKU=C:\\Program Files\\heroku\\bin']) {
                    withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                        bat 'docker login -u %username% -p %password% registry.heroku.com'
                    }
                    bat '$HEROKU\\heroku container:release web --app=test-api-9'
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
